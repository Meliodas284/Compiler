namespace Compilation.Interpreter.Lexer.Models;

/// <summary>
/// Токен (лексем).
/// </summary>
public sealed class Token
{
    /// <summary>
    /// Тип лексемы.
    /// </summary>
    public TokenType Type { get; }

    /// <summary>
    /// Лексема.
    /// </summary>
    public string Lexeme { get; }

    /// <summary>
    /// Строка, где находится лексема (для отслеживания ошибок).
    /// </summary>
    public int Line { get; }

    /// <summary>
    /// Номер лексемы в строке (для отслеживания ошибок).
    /// </summary>
    public int Column { get; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public Token(
        TokenType type,
        string lexeme,
        int line,
        int column)
    {
        Type = type;
        Lexeme = lexeme;
        Line = line;
        Column = column;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Type} '{Lexeme}' ({Line}:{Column})";
    }
}
