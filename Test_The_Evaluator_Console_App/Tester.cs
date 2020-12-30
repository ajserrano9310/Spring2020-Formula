using System;
using FormulaEvaluator;

/// <summary> 
/// Author:    Alejandro Serrano (u1214728)
/// Partner:   None
/// Date:      January 17, 2020 
/// Course:    CS 3500, University of Utah, School of Computing 
///  
/// I, Alejandro Serrano (u1214728), certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// The following code tests the Evaluate method that's inside the FormulaEvaluator class for correctness and for certain errors
/// that might ocurr if an expression is non valid, if there's division by zero, etc. 
/// 

namespace Test_The_Evaluator_Console_App
{
    class Tester
    {
        /// <summary>
        /// Tests for simple expressions without parentheses 
        /// </summary>
        /// <param name="expression"> expression to be evaluated </param>
        /// <param name="result"> result of the expression</param>
        private static void NoParenthesesTest(string expression, int result)
        {
            if(Evaluator.Evaluate(expression, null) == result)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Failed");
            }
        }
        /// <summary>
        /// Tests for expressions with variable but no parentheses
        /// </summary>
        /// <param name="expression"> expression to be evaluated </param>
        /// <param name="result"> result of the expression to be evaluated</param>
        private static void NoParenthesesTestWithVariables(string expression, int result)
        {
            if (Evaluator.Evaluate(expression, Lookup) == result)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Failed");
            }
        }
        /// <summary>
        /// Tests for expressions with variables and parentheses
        /// </summary>
        /// <param name="expression">expression to be evaluated</param>
        /// <param name="result">result of the expression</param>
        private static void ParenthesesTestWithVariables(string expression, int result)
        {
            if (Evaluator.Evaluate(expression, Lookup) == result)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Failed");
            }
        }
        /// <summary>
        /// Tests for expressions with parentheses
        /// </summary>
        /// <param name="expression">expression to be evaluated</param>
        /// <param name="result">result of the expression</param>
        private static void ParenthesesTest(string expression, int result)
        {
            if (Evaluator.Evaluate(expression, null) == result)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Failed");
            }
        }
        /// <summary>
        /// Tests for more complicated expressions
        /// </summary>
        /// <param name="expression">expression to be evaluated</param>
        /// <param name="result">result of the expression</param>
        private static void ComplicatedExpressions(string expression, int result)
        {
            if (Evaluator.Evaluate(expression, null) == result)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Failed");
            }
        }
        /// <summary>
        /// Looks for the corresponding value of the variable paramenter
        /// </summary>
        /// <param name="variable"> the string to be evaluated </param>
        /// <returns> returns the corresponding value related to the variable,
        /// otherwise the method throws an argument exception if the variable can't
        /// be found. 
        /// </returns>
        public static int Lookup (string variable)
        {
           char[] characters = variable.ToCharArray();
            if (variable.Length == 2)
            {
                // For the sake of this assignment, we will simply consider A (1 - 7)
                // as possible variables to be evaluated. Other cases will simply throw 
                // an argument exception error which can be seeing handled on the 
                // testing section: "Catching possible errors". 
                if (characters[0] != 'A')
                {
                    throw new ArgumentException("Variable not valid");
                }
                else if (characters[1] == '1')
                    return 10;
                else if (characters[1] == '2')
                    return 15;
                else if (characters[1] == '3')
                    return 20;
                else if (characters[1] == '4')
                    return 25;
                else if (characters[1] == '5')
                    return 30;
                else if (characters[1] == '6')
                    return 35;
                else if (characters[1] == '7')
                    return 40;
            }
            throw new ArgumentException("Variable not valid");
        }
        /// <summary>
        /// Main contains many different tests for the Evaluate method inside
        /// Formula Evaluator. The success output on the console states that the 
        /// expression was evaluated correctly. 
        /// Try and catch test cases will have a message stating what exactly was
        /// the problem with the expression. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine(" ");
            Console.WriteLine("Testing Variables without parentheses");
            Console.WriteLine(" ");

            NoParenthesesTestWithVariables("A1+1", 11);
            NoParenthesesTestWithVariables("A1+1 * 2", 12);
            NoParenthesesTestWithVariables("A1+A2 * 2", 40);

            Console.WriteLine(" ");
            Console.WriteLine("Testing Variables with parentheses");
            Console.WriteLine(" ");

            ParenthesesTestWithVariables("(2*A5)/10", 6);
            ParenthesesTestWithVariables("(A1*A5)/A2", 20);
            ParenthesesTestWithVariables("(2*A5)/10", 6);

            Console.WriteLine("No parentheses tests");

            NoParenthesesTest("1+1", 2);
            NoParenthesesTest("1+2+3", 6);
            NoParenthesesTest("1-2+3", 2);
            NoParenthesesTest("2*2", 4);
            NoParenthesesTest("2*0", 0);
            NoParenthesesTest("2*2+1", 5);
            NoParenthesesTest("1+3*5", 16);
            NoParenthesesTest("1+15/5", 4);
            NoParenthesesTest("20/2+1", 11);
            NoParenthesesTest("1*1 + 1 + 2", 4);

            Console.WriteLine(" ");
            Console.WriteLine("Test: Parentheses tests");
            Console.WriteLine(" ");

            ParenthesesTest("(5+5)", 10);
            ParenthesesTest("(1*1)", 1);
            ParenthesesTest("(1*1)+1", 2);
            ParenthesesTest("(1*1)+1 + 2", 4);
            ParenthesesTest("(5*(3+2))", 25);

            Console.WriteLine(" ");
            Console.WriteLine("Test: More Complicated Expressions.");
            Console.WriteLine(" ");

            ComplicatedExpressions("(30*2) - (4*(4+1))/2*2", 40);
            ComplicatedExpressions("(30*2) - 4*(4+1)/2*2", 40);
            ComplicatedExpressions("100*(25/5)+30/6+10", 515);
            ComplicatedExpressions("(2 * 3) / 2 * 10 + (10 - 1)", 39);
            ComplicatedExpressions("(2 + 3) * 5 + 2", 27);
            ComplicatedExpressions("2 + 5*(2 + 3)", 27);
            ComplicatedExpressions("(3-1)*5+2", 12);

            Console.WriteLine(" ");
            Console.WriteLine("Test: Catching possible erros.");
            Console.WriteLine(" ");

            try
            {
                FormulaEvaluator.Evaluator.Evaluate("1+1)", null);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("No left Parentheses");
            }

            try
            {
                FormulaEvaluator.Evaluator.Evaluate("1/0", null);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Can not perform division by zero");
            }

            try
            {
                FormulaEvaluator.Evaluator.Evaluate("(1 + 2 + 5", null);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("No right parentheses");
            }

            try
            {
                FormulaEvaluator.Evaluator.Evaluate("1 + 2 + ", null);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Incomplete expression.");
            }

            try
            {
                FormulaEvaluator.Evaluator.Evaluate("1 + 2 + # ", null);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid value inside expression.");
            }

            try
            {
                FormulaEvaluator.Evaluator.Evaluate("1 + 2 + ! ", null);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid value inside expression.");
            }

            try
            {
                Evaluator.Evaluate("(2*C5)/10", Lookup);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Not a valid variable");
            }
            Console.Read();
        }
    }
}
