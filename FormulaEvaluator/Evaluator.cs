using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
/// <summary> 
/// Author:    Alejandro Serrano (u1214728)
/// Partner:   None
/// Date:      January 17, 2020 
/// Course:    CS 3500, University of Utah, School of Computing 
///  
/// I, Alejandro Serrano (u1214728), certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// The following code evaluates infix expressions such as: "(2+1) * 2". In addition, it can take certain variables represented
/// in the Lookup function on the Tester class. 
/// 
namespace FormulaEvaluator
{
    public static class Evaluator
    {
        public delegate int Lookup(String variable_name);

        /// <summary>
        /// Helper method: Checks the variable var to see if the first character 
        /// fits the possibility to be considered a possible variable 
        /// for the operation.
        /// </summary>
        /// <param name="var"> string to be checked </param>
        /// <returns> True if it's considered a possible variable for the Lookup methdo</returns>
        private static Boolean IsVariable(string var)
        {
            
            bool checkForExpression = Regex.IsMatch(var, "^[a-zA-Z]+[0-9]+");

            if (checkForExpression)
            {
                return true;
            }

            return false;

         


            //string lettersUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //string lettersUnder = "abcdefghijklmnopqrstuvwxyz";
            //char[] lettersUpperToChar = lettersUpper.ToCharArray();
            //char[] lettersUnderToChar = lettersUnder.ToCharArray();

            //for (int j = 0; j < lettersUpperToChar.Length; j++)
            //{
            //    if((lettersUpperToChar[j] == var[0])||(lettersUnderToChar[j]==var[0]))
            //    {
            //        return true;
            //    }
            //}

            
        } 
        /// <summary>
        /// Helper Method: Checks to see if value is actually zero. 
        /// Otherwise throws error if it's any other value like:
        /// x, !, $, #, etc.
        /// </summary>
        /// <param name="token"> value to be evaluated</param>
        /// <param name="parsedInt"> int value of the token </param>
        /// <returns></returns>
        private static bool CheckForInvalidValues(string token, int parsedInt)
        {
            if(token.Equals("0")&&parsedInt == 0)
            {
                return true;
            }

            throw new ArgumentException("Invalid Expression");
        }
        /// <summary>
        /// The function handles infix expressions either with simple operations with integers
        /// or with certain established variables. 
        /// </summary>
        /// <param name="expression"> the expression to be evaluated </param>
        /// <param name="variableEvaluator"> function that will look for the respective
        /// variable inside the expression</param>
        /// <returns> returns the calculated operation from the expression </returns>
        public static int Evaluate(String expression, Lookup variableEvaluator){

            int operation = 0;
            Stack <int> numValues = new Stack <int>();
            Stack <string> operators = new Stack <string>();
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            for(int i = 0; i < substrings.Length; i++)
            {
                substrings[i] = substrings[i].Trim();
            }


            for (int i = 0; i < substrings.Length; i++)
            {
                string token = substrings[i];

                // First two checks take care of if the token is a whitespace 
                // or if it's variable. Afterwards, the code handle the most common
                // cases
                if (!(token.Equals("") || token.Equals(" ")))
                {
                    // If it's neither, then we start evaluating the actual operations and possible integers. 
                    // The instructions are followed as provided in the Assignment 01 writeup. 
                    if (!token.Equals("+") && !token.Equals("-") && !token.Equals("/") && !token.Equals("*") && !token.Equals(")") && !token.Equals("(")&&!IsVariable(token))
                    {

                        int x;

                        bool a = Int32.TryParse(token, out x);

                        // Considering that some tokens like "!, %, $" parse into 0, 
                        // this can be taken into advantage and check if the token is 
                        // actually zero, or if it's another symbol. 
                        // Therefore, we check. If it's zero, the code continues with no issues, 
                        // otherwise, throws an Argument Error. 
                        if (x == 0)
                            CheckForInvalidValues(token, x);

                        if (numValues.Count == 0)
                        {
                            numValues.Push(x);
                        }
                        else
                        {
                            if (operators.Count != 0)
                            {
                                if (numValues.Count == 0)
                                {
                                    throw new ArgumentException("Value Stack empty");
                                }

                                if (operators.Peek().Equals("*"))
                                {
                                    int y = numValues.Pop();
                                    operators.Pop();
                                    operation = x * y;
                                    numValues.Push(operation);
                                }

                                else if (operators.Peek().Equals("/"))
                                {
                                    int y = numValues.Pop();

                                    if (x == 0)
                                    {
                                        throw new ArgumentException("Can not divide by zero");
                                    }
                                    else
                                    {
                                        operators.Pop();
                                        operation = y / x;
                                        numValues.Push(operation);
                                    }
                                }
                                else
                                {
                                    numValues.Push(x);
                                }


                            }
                        }
                    }

                    else if (token.Equals("+"))
                    {
                        if (operators.Count != 0)
                        {
                            if (operators.Peek().Equals("+"))
                            {
                                if (numValues.Count < 2)
                                {
                                    throw new ArgumentException("Can not perform operation. Not enough values.");
                                }
                                else
                                {
                                    int y = numValues.Pop();
                                    int x = numValues.Pop();
                                    operators.Pop();
                                    operation = x + y;
                                    numValues.Push(operation);
                                    operators.Push(token);
                                }
                            }
                            else if (operators.Peek().Equals("-"))
                            {
                                if (numValues.Count < 2)
                                {
                                    throw new ArgumentException("Can not perform operation. Not enough values.");
                                }
                                else
                                {
                                    int y = numValues.Pop();
                                    int x = numValues.Pop();
                                    operators.Pop();
                                    operation = x - y;
                                    numValues.Push(operation);
                                    operators.Push(token);
                                }
                            }
                            else
                            {
                                operators.Push(token);
                            }
                        }
                        else
                        {
                            operators.Push(token);
                        }

                    }
                    else if (token.Equals("-"))
                    {
                        if (operators.Count != 0)
                        {
                            if (operators.Peek().Equals("+"))
                            {

                                int y = numValues.Pop();
                                int x = numValues.Pop();
                                operators.Pop();
                                operation = x + y;
                                numValues.Push(operation);
                                operators.Push(token);

                            }
                            else if (operators.Peek().Equals("-"))
                            {
                                int y = numValues.Pop();
                                int x = numValues.Pop();
                                operators.Pop();
                                operation = x - y;
                                numValues.Push(operation);
                                operators.Push(token);
                            }
                            else
                            {
                                operators.Push(token);
                            }
                        }
                        else
                        {
                            operators.Push(token);
                        }
                    }
                    // Parentheses
                    else if (token.Equals("("))
                    {
                        operators.Push(token);
                    }
                    // Pushing multiplication and division operators
                    else if (token.Equals("*"))
                    {
                        operators.Push(token);
                    }
                    else if (token.Equals("/"))
                    {
                        operators.Push(token);
                    }
                    // Closing parentheses
                    else if (token.Equals(")"))
                    {
                        if (!operators.Contains("("))
                        {
                            throw new ArgumentException("No left parentheses");
                        }
                        if (operators.Peek().Equals("+"))
                        {
                            int y = numValues.Pop();
                            int x = numValues.Pop();
                            operators.Pop();
                            operation = x + y;
                            numValues.Push(operation);

                        }
                        else if (operators.Peek().Equals("-"))
                        {
                            int y = numValues.Pop();
                            int x = numValues.Pop();
                            operators.Pop();
                            operation = x - y;
                            numValues.Push(operation);

                        }

                        operators.Pop();

                        if (operators.Count != 0 && operators.Peek().Equals("*"))
                        {
                            int y = numValues.Pop();
                            int x = numValues.Pop();
                            operators.Pop();
                            operation = x * y;
                            numValues.Push(operation);
                        }

                        else if (operators.Count != 0 && operators.Peek().Equals("/"))
                        {
                            int y = numValues.Pop();
                            int x = numValues.Pop();
                            if (y == 0)
                                throw new DivideByZeroException("Can not divide by zero");
                            else
                            {
                                operators.Pop();
                                operation = x / y;
                                numValues.Push(operation);
                            }
                        }
                    }
                    else
                    {
                        if (IsVariable(token))
                        {
                            int variableValue = variableEvaluator(token);
                            if (operators.Count != 0)
                            {
                                if (operators.Peek().Equals("*"))
                                {
                                    int y = numValues.Pop();
                                    operators.Pop();
                                    operation = variableValue * y;
                                    numValues.Push(operation);
                                }

                                else if (operators.Peek().Equals("/"))
                                {
                                    int y = numValues.Pop();

                                    if (variableValue == 0)
                                    {
                                        throw new ArgumentException("Can not divide by zero");
                                    }
                                    else
                                    {
                                        operators.Pop();
                                        operation = y / variableValue;
                                        numValues.Push(operation);
                                    }
                                }
                                else
                                {
                                    numValues.Push(variableValue);
                                }
                            }
                            else
                            {
                                numValues.Push(variableValue);
                            }
                        }
                        else
                        {
                            throw new ArgumentException("dafdf");
                        }
                    }
                }
            }

            // The final two statments handle the case of if there's some residual 
            // work to be done. Otherwise, here we also consider certain errors that might
            // ocurr from evaluating a wrong infix expression. For instance, if the input string
            // is "2 + 2 +". 
            if (operators.Count == 0)
            {
                if (numValues.Count == 1)
                {
                    return numValues.Pop();
                }
                else
                {
                    throw new ArgumentException("More than one value in stack.");
                }

            }
            else if(operators.Count == 1 && numValues.Count == 2)
            {
                if (operators.Peek().Equals("-"))
                {
                    int y = numValues.Pop();
                    int x = numValues.Pop();
                    operators.Pop();
                    operation = x - y;
                    numValues.Push(operation);
                    return numValues.Pop();
                }
                else if (operators.Peek().Equals("+"))
                {
                    int y = numValues.Pop();
                    int x = numValues.Pop();
                    operators.Pop();
                    operation = x + y;
                    numValues.Push(operation);
                    return numValues.Pop();
                }
            }
            else
            {
                throw new ArgumentException("Could not evaluate expression.");
            }

            return numValues.Pop();
        }
    }
}
