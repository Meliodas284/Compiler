using Compilation.Interpreter.Lexer.Models;

namespace Compilation.Interpreter.Lexer;

/// <summary>
/// Классификатор входящих символов по классам.
/// </summary>
public static class LexerCharClassifier
{
    /// <summary>
    /// Получить класс символа.
    /// </summary>
    /// <param name="c"> Входящий символ. </param>
    /// <returns> Класс символа. </returns>
    public static CharClass GetCharClass(char c)
    {
        if (char.IsLetter(c))
            return CharClass.Letter;

        if (char.IsDigit(c))
            return CharClass.Digit;

        return c switch
        {
            '.' => CharClass.Dot,

            ' ' or '\t' or '\r' or '\n'
                => CharClass.WhiteSpace,

            '+' => CharClass.Plus,
            '-' => CharClass.Minus,
            '*' => CharClass.Star,
            '/' => CharClass.Slash,

            '=' => CharClass.Equal,
            '!' => CharClass.Exclamation,

            '<' => CharClass.Less,
            '>' => CharClass.Greater,

            '(' => CharClass.LeftParen,
            ')' => CharClass.RightParen,

            '{' => CharClass.LeftBrace,
            '}' => CharClass.RightBrace,

            '[' => CharClass.LeftBracket,
            ']' => CharClass.RightBracket,

            ';' => CharClass.Semicolon,
            ',' => CharClass.Comma,

            '\0' => CharClass.EOF,

            _ => CharClass.Other
        };
    }
}
