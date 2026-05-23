using Compilation.Interpreter.Lexer;
using Compilation.Interpreter.Lexer.Models;

string source = """
abc@ = array(10);
""";

Lexer lexer = new(source);

Token token;

do
{
    token = lexer.NextToken();

    Console.WriteLine(token);

} while (token.Type != TokenType.EOF);