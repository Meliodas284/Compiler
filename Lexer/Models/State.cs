namespace Compilation.Interpreter.Lexer.Models;

/// <summary>
/// Состояния автомата.
/// </summary>
public enum State
{
    // Старт
    S,

    // Идентификатор
    I,

    // Целое число
    N,

    // Число с дробной частью
    F,

    // =
    A,

    // ==
    B,

    // !
    C,

    // !=
    D,

    // <
    E,

    // <=
    G,

    // >
    H,

    // >=
    K,

    // Финал
    Z,

    // Ошибка
    ERR
}
