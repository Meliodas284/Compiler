using Compilation.Interpreter.Lexer.Models;
using System.Text;

namespace Compilation.Interpreter.Lexer;

/// <summary>
/// Лексер.
/// </summary>
public sealed class Lexer
{
    /// <summary>
    /// Входящий набор текста.
    /// </summary>
    private readonly string _source;

    /// <summary>
    /// Позиция.
    /// </summary>
    private int _position;

    /// <summary>
    /// Строка.
    /// </summary>
    private int _line = 1;

    /// <summary>
    /// Номер текущего символа в строке.
    /// </summary>
    private int _column = 1;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public Lexer(string source)
    {
        _source = source;
    }

    /// <summary>
    /// Получить следущий токен.
    /// </summary>
    /// <returns> Объект токена. </returns>
    public Token NextToken()
    {
        var state = State.S;

        StringBuilder lexemeBuilder = new();

        var tokenLine = _line;
        var tokenColumn = _column;

        while (true)
        {
            var currentChar = PeekChar();

            var charClass =
                LexerCharClassifier.GetCharClass(currentChar);

            var nextState =
                DfaTransitionTable.GetNextState(
                    state,
                    charClass);

            // Ошибка DFA
            if (nextState == State.ERR)
            {
                // Специальный случай:
                // финализация токена через lookahead
                if (state == State.I ||
                    state == State.N ||
                    state == State.F ||
                    state == State.A ||
                    state == State.B ||
                    state == State.D ||
                    state == State.E ||
                    state == State.G ||
                    state == State.H ||
                    state == State.K)
                {
                    return BuildToken(
                        state,
                        lexemeBuilder.ToString(),
                        tokenLine,
                        tokenColumn);
                }

                throw new Exception(
                    $"Ошибка распознования лексем в строке {_line}, позиции {_column}");
            }

            // Пропуск whitespace
            if (state == State.S &&
                charClass == CharClass.WhiteSpace)
            {
                Advance(currentChar);

                tokenLine = _line;
                tokenColumn = _column;

                continue;
            }

            // Односимвольные токены
            if (state == State.S &&
                nextState == State.Z)
            {
                Advance(currentChar);

                return BuildSingleCharToken(
                    currentChar,
                    tokenLine,
                    tokenColumn);
            }

            // EOF
            if (charClass == CharClass.EOF)
            {
                return new Token(
                    TokenType.EOF,
                    string.Empty,
                    _line,
                    _column);
            }

            // Переход в новое состояние
            state = nextState;

            lexemeBuilder.Append(currentChar);

            Advance(currentChar);
        }
    }

    /// <summary>
    /// Получить следующий символ.
    /// </summary>
    /// <returns> Слежующий символ. </returns>
    private char PeekChar()
    {
        if (_position >= _source.Length)
            return '\0';

        return _source[_position];
    }

    /// <summary>
    /// Перейти к следующему символу.
    /// </summary>
    /// <param name="c"> Следующий символ. </param>
    private void Advance(char c)
    {
        _position++;

        if (c == '\n')
        {
            _line++;
            _column = 1;
        }
        else
        {
            _column++;
        }
    }

    /// <summary>
    /// Построить лексему.
    /// </summary>
    /// <param name="state"> Текущее состояние. </param>
    /// <param name="lexeme"> Лексема. </param>
    /// <param name="line"> Строка. </param>
    /// <param name="column"> Позиция в строке. </param>
    /// <returns> Объект лексемы. </returns>
    private static Token BuildToken(
        State state,
        string lexeme,
        int line,
        int column)
    {
        return state switch
        {
            State.I => BuildIdentifierOrKeyword(
                lexeme,
                line,
                column),

            State.N or State.F =>
                new Token(
                    TokenType.Number,
                    lexeme,
                    line,
                    column),

            State.A =>
                new Token(
                    TokenType.Assign,
                    lexeme,
                    line,
                    column),

            State.B =>
                new Token(
                    TokenType.Equal,
                    lexeme,
                    line,
                    column),

            State.D =>
                new Token(
                    TokenType.NotEqual,
                    lexeme,
                    line,
                    column),

            State.E =>
                new Token(
                    TokenType.Less,
                    lexeme,
                    line,
                    column),

            State.G =>
                new Token(
                    TokenType.LessOrEqual,
                    lexeme,
                    line,
                    column),

            State.H =>
                new Token(
                    TokenType.Greater,
                    lexeme,
                    line,
                    column),

            State.K =>
                new Token(
                    TokenType.GreaterOrEqual,
                    lexeme,
                    line,
                    column),

            _ => throw new Exception(
                $"Unknown token state: {state}")
        };
    }

    /// <summary>
    /// Построить лексему ключевого слова.
    /// </summary>
    /// <param name="lexeme"> Лексема. </param>
    /// <param name="line"> Строка. </param>
    /// <param name="column"> Позиция в строке. </param>
    /// <returns> Объект лексемы. </returns>
    private static Token BuildIdentifierOrKeyword(
        string lexeme,
        int line,
        int column)
    {
        TokenType type = lexeme switch
        {
            "if" => TokenType.If,
            "else" => TokenType.Else,
            "while" => TokenType.While,

            "read" => TokenType.Read,
            "write" => TokenType.Write,

            "sqrt" => TokenType.Sqrt,
            "exp" => TokenType.Exp,
            "log" => TokenType.Log,

            "array" => TokenType.Array,

            _ => TokenType.Identifier
        };

        return new Token(type, lexeme, line, column);
    }

    /// <summary>
    /// Построить лексему одиночных символов.
    /// </summary>
    /// <param name="c"> Входящий символ. </param>
    /// <param name="line"> Строка. </param>
    /// <param name="column"> Позиция в строке. </param>
    /// <returns></returns>
    private static Token BuildSingleCharToken(
        char c,
        int line,
        int column)
    {
        return c switch
        {
            '+' => new Token(TokenType.Plus, "+", line, column),
            '-' => new Token(TokenType.Minus, "-", line, column),

            '*' => new Token(TokenType.Multiply, "*", line, column),
            '/' => new Token(TokenType.Divide, "/", line, column),

            '(' => new Token(TokenType.LeftParen, "(", line, column),
            ')' => new Token(TokenType.RightParen, ")", line, column),

            '{' => new Token(TokenType.LeftBrace, "{", line, column),
            '}' => new Token(TokenType.RightBrace, "}", line, column),

            '[' => new Token(TokenType.LeftBracket, "[", line, column),
            ']' => new Token(TokenType.RightBracket, "]", line, column),

            ';' => new Token(TokenType.Semicolon, ";", line, column),

            ',' => new Token(TokenType.Comma, ",", line, column),

            '\0' => new Token(TokenType.EOF, "", line, column),

            _ => throw new Exception(
                $"Unknown single-char token: {c}")
        };
    }
}