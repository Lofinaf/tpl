// Triton programming languange

Scheme:
	[Source code---->---Tokenization(Lexer.dll)----------|]
	[|--Tokens(Lexer.ToTokens() returnes List<string>)--<-]
	[->------------------>----------------->--------->-------|]
	[Run Command(CommandRate)-----<-----Interplit. foreach(tokens, i, v)-<-]

sln:
	|
	|
	Lexer.dll
	|	|
	|	|_______ enum Tokens;
	|	|_______ List<string> Tokenization();
	|
	|
	tpl.exe
	|	|
	|	|
	|	|
	|	|_______ void Readargs();
	|	|_______ Compiler.cs
	|	|		 |
	|	|		 |_____ Stack<> for stack tpl
	|	|		 |_____ Dictionary<> for variables
	|	|		 |_____ And other tools for tlp
	|	|_______ enum argstype;
	|	|_______ void Main();
	|
	|
	CommandRate.dll();

Credit:	
	Developers Ridick(tg: @kian), Lofinaf(tg: @descis)
	Version 1.0.0.0
	Current scheme by Lofinaf for triton programming languange