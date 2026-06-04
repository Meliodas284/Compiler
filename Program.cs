using Compilation.Interpreter.Lexer;
using Compilation.Interpreter.Lexer.Models;

string source = """
a = 10;
i = 0;
while (i < a) { i = i + 1; }
""";

Lexer lexer = new(source);

Token token;

do
{
    token = lexer.NextToken();

    Console.WriteLine(token);

} while (token.Type != TokenType.EOF);