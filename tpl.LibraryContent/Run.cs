using System;
using System.Collections.Generic;
using tpl.Runtime.Results;

using static tpl.LibraryContent.Exception;

namespace tpl.LibraryContent
{
    public partial class Run
    {
        public static InterpreterResult ReadTokens(List<string> Tokens, ref Stack<string> Stack, ref Dictionary<string, string> Variables, Param param)
        {
            var Result = new InterpreterResult();

            int line = 1;
            for (int pos = 0; pos < Tokens.Count; pos++)
            {
                var token = Tokens[pos];
                if (param == Param.debug)
                {
                    Console.WriteLine($"Token: {token}, Position: {pos}; Tokens count: {Tokens.Count}");
                }
                switch (token)
                {

                    case "s_space":
                        line++;
                        break;

                    case "o_sign":
                        if (Variables.TryGetValue(Tokens[pos-1], out string leftSign) && Variables.TryGetValue(Tokens[pos+1], out string rightSign))
                        {
                            Variables[Tokens[pos - 1]] = Variables[Tokens[pos + 1]];
                            break;
                        }
                        if (Variables.TryGetValue(Tokens[pos - 1], out string leftVarSign))
                        {
                            Variables[Tokens[pos - 1]] = Tokens[pos + 1];
                            break;
                        }
                        ThrowError(ref Result ,ThrowErrors.OPERATORERR, line, Tokens[pos], 5);
                        break;

                    case "o_plus":
                        if (Variables.TryGetValue(Tokens[pos - 1], out string leftPlus) && Variables.TryGetValue(Tokens[pos + 1], out string rightPlus))
                        {
                            Variables[Tokens[pos - 1]] += Variables[Tokens[pos + 1]];
                            break;
                        }
                        if (Variables.TryGetValue(Tokens[pos - 1], out string leftVarPlus))
                        {
                            if (Tokens[pos + 1].StartsWith("N"))
                            {
                                if (int.TryParse(Tokens[pos+1], out int rightNumPlus) && (int.TryParse(Variables[Tokens[pos - 1]], out int leftNumPlus)))
                                {
                                    int value = rightNumPlus + leftNumPlus;
                                    Variables[Tokens[pos - 1]] = value.ToString();
                                }
                                break;
                            }
                            Variables[Tokens[pos - 1]] += Tokens[pos + 1];
                            break;
                        }
                        ThrowError(ref Result, ThrowErrors.OPERATORERR, line, Tokens[pos], 4);
                        break;

                    case "k_var":
                        try
                        {
                            Variables.Add(Tokens[pos + 1], "null");
                        }
                        catch (System.Exception)
                        {
                            ThrowError(ref Result, ThrowErrors.VARCANBEDECLARED, line, Tokens[pos], 5);
                        }
                        break;

                    case "PRINT":
                        if (Tokens[pos+1] == "lsq")
                        {
                            string ValueToWrite = "";
                            bool isFirstWord = true;
                            bool isText = false;
                            for (int word = pos + 1; word <= _maxWords; word++)
                            {
                                if (Tokens.Count > word)
                                {
                                    var Token = Tokens[word];
                                    if (Token == "rsq")
                                    {
                                        break;
                                    }
                                    if (Token == "s_symbol")
                                    {
                                        if (isText)
                                        {
                                            isText = false;
                                            continue;
                                        }
                                        isText = true;
                                        continue;
                                    }
                                    if (!IsTokenKey(Token) && isFirstWord)
                                    {
                                        if (isText)
                                        {
                                            isFirstWord = false;
                                            ValueToWrite += $"{Token}";
                                            continue;
                                        }
                                        try
                                        {
                                            ValueToWrite += $"{Variables[Token]}";
                                        }
                                        catch (System.Exception)
                                        {
                                            ThrowError(ref Result, ThrowErrors.INTYPEISSTRING, line, Token, 3);
                                        }
                                        continue;
                                    }
                                    if (!IsTokenKey(Token))
                                    {
                                        if (isText)
                                        {
                                            isFirstWord = false;
                                            ValueToWrite += $" {Token}";
                                            continue;
                                        }
                                        try
                                        {
                                            ValueToWrite += $" {Variables[Token]}";
                                        }
                                        catch (System.Exception)
                                        {
                                            ThrowError(ref Result, ThrowErrors.INTYPEISSTRING, line, Token, 10);
                                        }
                                        continue;
                                    }
                                    continue;
                                }
                                break;
                            }
                            Console.WriteLine(ValueToWrite);
                            break;
                        }
                        ThrowError(ref Result, ThrowErrors.LSQNOTFOUND, 2);
                        return Result;

                    default:
                        break;
                }
            }
            return Result;
        }
    }
}
