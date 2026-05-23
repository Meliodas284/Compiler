namespace Compilation.Interpreter.Lexer.Models;

/// <summary>
/// Тип токена (лексемы).
/// </summary>
public enum TokenType
{
    // Идентификаторы и числа
    Identifier,
    Number,

    // Арифметика
    Plus,
    Minus,
    Multiply,
    Divide,

    // Присваивание
    Assign,

    // Сравнения
    Equal,
    NotEqual,
    Less,
    LessOrEqual,
    Greater,
    GreaterOrEqual,

    // Скобки
    LeftParen,
    RightParen,

    LeftBrace,
    RightBrace,

    LeftBracket,
    RightBracket,

    // Разделители
    Semicolon,
    Comma,

    // Ключевые слова
    If,
    Else,
    While,

    Read,
    Write,

    Sqrt,
    Exp,
    Log,
    Array,

    // Конец файла
    EOF
}
