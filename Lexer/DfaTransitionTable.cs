using Compilation.Interpreter.Lexer.Models;

namespace Compilation.Interpreter.Lexer;

/// <summary>
/// Таблица переходов состояний.
/// </summary>
public static class DfaTransitionTable
{
    /// <summary>
    /// Словарь переходов: 
    /// ключ - текущее состояние и входящий символ; значение - следующее состояние.
    /// </summary>
    /// <remarks>
    /// Состояния здесь: <see cref="State"/>
    /// </remarks>
    private static readonly Dictionary<
        (State, CharClass),
        State> _transitions = new()
    {
        // Состояние S.
        { (State.S, CharClass.Letter), State.I },
        { (State.S, CharClass.Digit), State.N },

        { (State.S, CharClass.WhiteSpace), State.S },

        { (State.S, CharClass.Equal), State.A },
        { (State.S, CharClass.Exclamation), State.C },

        { (State.S, CharClass.Less), State.E },
        { (State.S, CharClass.Greater), State.H },

        { (State.S, CharClass.Plus), State.Z },
        { (State.S, CharClass.Minus), State.Z },
        { (State.S, CharClass.Star), State.Z },
        { (State.S, CharClass.Slash), State.Z },

        { (State.S, CharClass.LeftParen), State.Z },
        { (State.S, CharClass.RightParen), State.Z },

        { (State.S, CharClass.LeftBrace), State.Z },
        { (State.S, CharClass.RightBrace), State.Z },

        { (State.S, CharClass.LeftBracket), State.Z },
        { (State.S, CharClass.RightBracket), State.Z },

        { (State.S, CharClass.Semicolon), State.Z },
        { (State.S, CharClass.Comma), State.Z },

        { (State.S, CharClass.EOF), State.Z },

        // Состояние I.
        { (State.I, CharClass.Letter), State.I },
        { (State.I, CharClass.Digit), State.I },

        // Состояние N.
        { (State.N, CharClass.Digit), State.N },
        { (State.N, CharClass.Dot), State.F },

        // Состояние F.
        { (State.F, CharClass.Digit), State.F },

        // Состояние A.
        { (State.A, CharClass.Equal), State.B },

        // Состояние C.
        { (State.C, CharClass.Equal), State.D },

        // Состояние E.
        { (State.E, CharClass.Equal), State.G },

        // Состояние H.
        { (State.H, CharClass.Equal), State.K },
    };

    /// <summary>
    /// Получить следующее состояние.
    /// </summary>
    /// <param name="currentState"> Текущее состояние. </param>
    /// <param name="charClass"> Входящий символ. </param>
    /// <returns> Следующее состояние. </returns>
    public static State GetNextState(
        State currentState,
        CharClass charClass)
    {
        return _transitions.TryGetValue(
            (currentState, charClass),
            out var nextState)
            ? nextState
            : State.ERR;
    }
}