// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

// (Daniel Kopta) 
// Version 1.2 (9/10/17) 

// Change log:
//  (Version 1.2) Changed the definition of equality with regards
//                to numeric tokens


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
/// Author:    Alejandro Serrano (u1214728)
/// Partner:   None
/// Date:      January 31, 2020 
/// Course:    CS 3500, University of Utah, School of Computing 
///  
/// I, Alejandro Serrano (u1214728), aside from the starter code given to us, 
/// certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
namespace SpreadsheetUtilities
{
  /// <summary>
  /// Represents formulas written in standard infix notation using standard precedence
  /// rules.  The allowed symbols are non-negative numbers written using double-precision 
  /// floating-point syntax (without unary preceeding '-' or '+'); 
  /// variables that consist of a letter or underscore followed by 
  /// zero or more letters, underscores, or digits; parentheses; and the four operator 
  /// symbols +, -, *, and /.  
  /// 
  /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
  /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable; 
  /// and "x 23" consists of a variable "x" and a number "23".
  /// 
  /// Associated with every formula are two delegates:  a normalizer and a validator.  The
  /// normalizer is used to convert variables into a canonical form, and the validator is used
  /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
  /// that it consist of a letter or underscore followed by zero or more letters, underscores,
  /// or digits.)  Their use is described in detail in the constructor and method comments.
  /// </summary>
  /// 
 
  public class Formula
  {
        /*
         * Private Instance variable that will hold
         * the final formula. 
         */
        private string form;
    /// <summary>
    /// Creates a Formula from a string that consists of an infix expression written as
    /// described in the class comment.  If the expression is syntactically invalid,
    /// throws a FormulaFormatException with an explanatory Message.
    /// 
    /// The associated normalizer is the identity function, and the associated validator
    /// maps every string to true.  
    /// </summary>
    public Formula(String formula) :
        this(formula, s => s, s => true)
    {
    }

    /// <summary>
    /// Creates a Formula from a string that consists of an infix expression written as
    /// described in the class comment.  If the expression is syntactically incorrect,
    /// throws a FormulaFormatException with an explanatory Message.
    /// 
    /// The associated normalizer and validator are the second and third parameters,
    /// respectively.  
    /// 
    /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
    /// throws a FormulaFormatException with an explanatory message. 
    /// 
    /// If the formula contains a variable v such that isValid(normalize(v)) is false,
    /// throws a FormulaFormatException with an explanatory message.
    /// 
    /// Suppose that N is a method that converts all the letters in a string to upper case, and
    /// that V is a method that returns true only if a string consists of one letter followed
    /// by one digit.  Then:
    /// 
    /// new Formula("x2+y3", N, V) should succeed
    /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
    /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
    /// </summary>
    public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
    {
            if(formula is null)
            {
                throw new ArgumentNullException("Formula is null");
            }
            string[] substrings = GetTokens(formula).ToArray();

            if(substrings.Length == 0)
            {
                throw new FormulaFormatException("Formula is empty");
            }

            if (StartingTokenCheck(substrings[0])) {

                for (int i = 0; i < substrings.Length-1; i++)
                {
                    string token = substrings[i];
                    if (Regex.IsMatch(token, "^[a-zA-Z]"))
                    {
                        CheckForOperators(substrings[i + 1]);
                        string normVar = normalize(token);
                        isValid(normVar);

                        this.form = this.form + normVar;
                    }
                    else if (token.Equals("(") || token.Equals(")"))
                    {
                        CountParentheses(substrings);
                        FollowingRule(token, substrings[i + 1]);
                        this.form = this.form + token;
                    }
                    else if(token.Equals("+")|| token.Equals("-")|| 
                        token.Equals("*")|| token.Equals("/"))
                    {
                        FollowingRule(token, substrings[i + 1]);
                        this.form = this.form + token;
                        
                    }
                    else if(Regex.IsMatch(token, "^[0-9]"))
                    {
                        CheckForOperators(substrings[i + 1]);
                        double parsedDouble = Double.Parse(token);
                        this.form = this.form + parsedDouble;
                    }
                }
            }
            // Check for final value in array.
            if (Regex.IsMatch(substrings[substrings.Length-1], "^[a-zA-Z]") || substrings[substrings.Length-1].Equals(")") ||
               Regex.IsMatch(substrings[substrings.Length-1], "^[0-9]"))
            {
                if (Regex.IsMatch(substrings[substrings.Length-1], "^[a-zA-Z]"))
                {
                    string normVar = normalize(substrings[substrings.Length-1]);
                    isValid(normVar);
                    this.form = this.form + normVar;
                }
                else if (substrings[substrings.Length-1].Equals(")"))
                {
                    CountParentheses(substrings);
                    this.form = this.form + substrings[substrings.Length-1];
                }
                else
                {
                    this.form = this.form + substrings[substrings.Length - 1];   
                }
            }
            else
            {
                throw new FormulaFormatException("Invalid Syntax");
            }

        }

        // Private Methods

        /// <summary>
        /// Checks if the following token's value is 
        /// an appropriate operator. 
        /// </summary>
        /// <param name="token">token to be tested</param>
        /// <returns>true or false</returns>
        private static Boolean CheckForOperators(string token)
        {
            if(token.Equals("+")|| token.Equals("-")
                || token.Equals("*")|| token.Equals("/")||token.Equals(")"))
            {
                return true;
            }
            throw new FormulaFormatException("Wrong Syntax");
        }
        /// <summary>
        /// Checks to see if after each opera
        /// </summary>
        /// <param name="token">token to be tested</param>
        /// <returns>true or false</returns>
        private static Boolean FollowingRule(string prev, string token)
        {
            if (Regex.IsMatch(token, "^[a-zA-Z]") || token.Equals("(") ||
               Regex.IsMatch(token, "^[0-9]")||token.Equals(")"))
            {

                return true;
            }
            else if (prev.Equals(")"))
            {
                if (CheckForOperators(token))
                {
                    return true;
                }
                else
                {
                    throw new FormulaFormatException("Incorrect Format");
                }
                
            }
            else
            {
                throw new FormulaFormatException("Incorrect Format");
            }
           
        }
        /// <summary>
        /// Checks to see if the first value of the Formula is the
        /// correct one. 
        /// </summary>
        /// <param name="token">token to be evaluated</param>
        /// <returns></returns>
        private static Boolean StartingTokenCheck(string token)
        {
            if(Regex.IsMatch(token, "^[a-zA-Z]")||token.Equals("(")||
                Regex.IsMatch(token, "^[0-9]")){
                return true;
            }
            else
            {
                throw new FormulaFormatException("The starting value should be " +
               "either a number, left parentheses or variable.");
            }
           
        } 
        /// <summary>
        /// Counts the amount of parethenses, if the amount to the left
        /// is the same as the amount to the right.
        /// </summary>
        /// <param name="substring">array of tokens</param>
        /// <returns>True or false</returns>
        private static Boolean CountParentheses(string[] substring)
        {
            int rightParen = 0;
            int leftParen = 0;

            foreach(string token in substring)
            {
                if (token.Equals("("))
                {
                    leftParen++;
                }
                else if (token.Equals(")"))
                {
                    rightParen++;
                }
            }
            if(leftParen == rightParen)
            {
                return true;
            }
            throw new FormulaFormatException("Formula is not correct");
        }
        /// <summary>
        /// Does the multiplication or division for the particular operation
        /// </summary>
        /// <param name="x">operand</param>
        /// <param name="y">operand</param>
        /// <param name="e">operator</param>
        /// <returns>result of operation or FormulaError if it's division by Zero</returns>
        private static object CalculateMultOrDiv(double x, double y, string e)
        {
            double operation = 0.0;
            if (e.Equals("*"))
            {
                operation = x * y;
                return operation;
            }
            else
            {
                if (x == 0)
                    return new FormulaError();
                operation = y / x;
                return operation;
            }

        }
        /// <summary>
        /// Does the sum or substraction for the particular operation
        /// </summary>
        /// <param name="x">operand</param>
        /// <param name="y">operand</param>
        /// <param name="e">operator</param>
        /// <returns>result of operation</returns>
        public static double CalculateSumOrDifference(double x, double y, string e)
        {
            double operation = 0.0;
            if (e.Equals("+"))
            {
                operation = x + y;
                return operation;
            }
            else
            {

                operation = y - x;
                return operation;
            }

        }


        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
    {
            Stack<Double> numValues = new Stack<Double>();
            Stack<string> operators = new Stack<string>();
            double operation = 0;

            string[] substring = GetTokens(this.form).ToArray();

            foreach (string token in substring)
            {
                if (!token.Equals("+") && !token.Equals("-") && !token.Equals("/") && !token.Equals("*")
                    && !token.Equals(")") && !token.Equals("("))
                {
                 
                    double valueToEvaluate;
                    if(Regex.IsMatch(token, "^[a-zA-Z]"))
                    {
                        valueToEvaluate = lookup(token);
                    }
                    else
                    {
                        valueToEvaluate = Double.Parse(token);
                    }

                    if (numValues.Count == 0)
                    {
                        numValues.Push(valueToEvaluate);
                    }
                    else
                    {
                        if (operators.Count != 0)
                        {
                            // Checks for * or /
                            if (operators.HasOnTopMultOrDiv(operators.Peek()))
                            {
                                object formulaOrError = CalculateMultOrDiv(valueToEvaluate, numValues.Pop(), operators.Pop());
                                if (typeof(FormulaError).IsInstanceOfType(formulaOrError))
                                {
                                    return new FormulaError("Can not do division by zero");
                                }
                                else if (typeof(Double).IsInstanceOfType(formulaOrError))
                                {
                                    operation = (double)formulaOrError;
                                }
                                numValues.Push(operation);
                            }
                            else
                            {
                                numValues.Push(valueToEvaluate);
                            }
                        }
                    }
                }
                // For sum and difference
                // Checks if token == "+" or "-"
                else if (token.Equals("+")||token.Equals("-"))
                {
                    if (operators.Count != 0)
                    {
                        if (operators.HasOnTopSumOrDifference(operators.Peek()))
                        {
                            operation = CalculateSumOrDifference(numValues.Pop(), numValues.Pop(), operators.Pop());
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
                // PARENTHESES CHECK
                else if (token.Equals(")"))
                {
                    if (operators.HasOnTopSumOrDifference(operators.Peek()))
                    {
                        operation = CalculateSumOrDifference(numValues.Pop(), numValues.Pop(), operators.Pop());
                        numValues.Push(operation);
                    }

                    operators.Pop();

                    if (operators.Count != 0 && operators.HasOnTopMultOrDiv(operators.Peek()))
                    {
                        // Catch the error or operation in a object variable 
                        // Depending on which is which we check the type to obtain
                        // formulaError or the double operation
                        object formulaOrError = CalculateMultOrDiv(numValues.Pop(), numValues.Pop(), operators.Pop());
                        if (typeof(FormulaError).IsInstanceOfType(formulaOrError))
                        {
                            return new FormulaError("Can not do division by zero");
                        }
                        else if (typeof(Double).IsInstanceOfType(formulaOrError))
                        {
                            operation = (double)formulaOrError;
                        }
                       
                        numValues.Push(operation);
                    }
                }
            }

            if (operators.Count == 0)
            {
                if (numValues.Count == 1)
                {
                    return numValues.Pop();
                }

            }
            else if (operators.Count == 1 && numValues.Count == 2)
            {
                if (operators.Peek().Equals("-"))
                {
                    double y = numValues.Pop();
                    double x = numValues.Pop();
                    operators.Pop();
                    operation = x - y;
                    numValues.Push(operation);
                    return numValues.Pop();
                }
                else if (operators.Peek().Equals("+"))
                {
                    double y = numValues.Pop();
                    double x = numValues.Pop();
                    operators.Pop();
                    operation = x + y;
                    numValues.Push(operation);
                    return numValues.Pop();

                }
            }
            return numValues.Pop();
    }

    /// <summary>
    /// Enumerates the normalized versions of all of the variables that occur in this 
    /// formula.  No normalization may appear more than once in the enumeration, even 
    /// if it appears more than once in this Formula.
    /// 
    /// For example, if N is a method that converts all the letters in a string to upper case:
    /// 
    /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
    /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
    /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
    /// </summary>
    public IEnumerable<String> GetVariables()
    {
            // We will be holding the variables in a HashSet
            // and avoid duplicates
            HashSet<string> repeatedStrings = new HashSet<string>();
            string[] substring = GetTokens(this.form).ToArray();

            foreach(string token in substring)
            {
               if(Regex.IsMatch(token, "^[a-zA-Z]"))
                {
                    if (!repeatedStrings.Contains(token))
                    {
                        repeatedStrings.Add(token);
                    }
                    
                }
                
               
            }
            return repeatedStrings;
            
    }

    /// <summary>
    /// Returns a string containing no spaces which, if passed to the Formula
    /// constructor, will produce a Formula f such that this.Equals(f).  All of the
    /// variables in the string should be normalized.
    /// 
    /// For example, if N is a method that converts all the letters in a string to upper case:
    /// 
    /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
    /// new Formula("x + Y").ToString() should return "x+Y"
    /// </summary>
    public override string ToString()
    {
      return this.form;
    }

    /// <summary>
    /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
    /// whether or not this Formula and obj are equal.
    /// 
    /// Two Formulae are considered equal if they consist of the same tokens in the
    /// same order.  To determine token equality, all tokens are compared as strings 
    /// except for numeric tokens and variable tokens.
    /// Numeric tokens are considered equal if they are equal after being "normalized" 
    /// by C#'s standard conversion from string to double, then back to string. This 
    /// eliminates any inconsistencies due to limited floating point precision.
    /// Variable tokens are considered equal if their normalized forms are equal, as 
    /// defined by the provided normalizer.
    /// 
    /// For example, if N is a method that converts all the letters in a string to upper case:
    ///  
    /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
    /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
    /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
    /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
    /// </summary>
    public override bool Equals(object obj)
    {
            if(obj is null|| !typeof(Formula).IsInstanceOfType(obj))
            {
                return false;
            }

            Formula f = (Formula)obj;

            if(!(this.GetHashCode() == f.GetHashCode()))
            {
                return false;
            }
            else
            {
                string[] callingFormula = GetTokens(this.form).ToArray();
                string[] objFormula = GetTokens(f.ToString()).ToArray();

                for(int j = 0; j<callingFormula.Length; j++)
                {
                    if(Regex.IsMatch(callingFormula[j], "^[0-9]"))
                    {
                        double val = Double.Parse(callingFormula[j]);
                        double val2 = Double.Parse(objFormula[j]);

                        if(!(val == val2)){
                            return false;
                        }
                    }
                    else if (!(callingFormula[j].Equals(objFormula[j]))){
                        return false;
                    }
                }
                return true;
            }
     
    }

    /// <summary>
    /// Reports whether f1 == f2, using the notion of equality from the Equals method.
    /// Note that if both f1 and f2 are null, this method should return true.  If one is
    /// null and one is not, this method should return false.
    /// </summary>
    public static bool operator ==(Formula f1, Formula f2)
    {
            if ((f1 is null) && (f2 is null))
            {
                return true;
            }
            return f1.Equals(f2);
    }

    /// <summary>
    /// Reports whether f1 != f2, using the notion of equality from the Equals method.
    /// Note that if both f1 and f2 are null, this method should return false.  If one is
    /// null and one is not, this method should return true.
    /// </summary>
    public static bool operator !=(Formula f1, Formula f2)
    {
            if ((f1 is null) && !(f2 is null))
            {
                return true;
            }

            return !f1.Equals(f2);
    }

    /// <summary>
    /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
    /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
    /// randomly-generated unequal Formulae have the same hash code should be extremely small.
    /// </summary>
    public override int GetHashCode()
    {
            // First we create the formula to tokens that will go to a string
            // Second, we create an array of characters to add every single ascii value
            // to create the HashCode to finally divide by a prime number. 
            string formForHashCode = "";
            int hashValue = 0;
            string[] substring = GetTokens(this.form).ToArray();
            foreach(string token in substring)
            {
                if(Regex.IsMatch(token, "^[0-9]"))
                {
                    string doubleToken = Double.Parse(token).ToString();
                    formForHashCode = formForHashCode + doubleToken;
                }
                else
                {
                    formForHashCode = formForHashCode + token;
                }
               
            }
            char[] charsForAscii = formForHashCode.ToCharArray();
            for (int j = 0; j < charsForAscii.Length; j++)
            {
                hashValue = hashValue+charsForAscii[j];
            }

      return hashValue/13;
    }

    /// <summary>
    /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
    /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
    /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
    /// match one of those patterns.  There are no empty tokens, and no token contains white space.
    /// </summary>
    private static IEnumerable<string> GetTokens(String formula)
    {
      // Patterns for individual tokens
      String lpPattern = @"\(";
      String rpPattern = @"\)";
      String opPattern = @"[\+\-*/]";
      String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
      String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
      String spacePattern = @"\s+";

      // Overall pattern
      String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                      lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

      // Enumerate matching tokens that don't consist solely of white space.
      foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
      {
        if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
        {
          yield return s;
        }
      }

    }
  }

  /// <summary>
  /// Used to report syntactic errors in the argument to the Formula constructor.
  /// </summary>
  public class FormulaFormatException : Exception
  {
    /// <summary>
    /// Constructs a FormulaFormatException containing the explanatory message.
    /// </summary>
    public FormulaFormatException(String message)
        : base(message)
    {
    }
  }

  /// <summary>
  /// Used as a possible return value of the Formula.Evaluate method.
  /// </summary>
  public struct FormulaError
  {
    /// <summary>
    /// Constructs a FormulaError containing the explanatory reason.
    /// </summary>
    /// <param name="reason"></param>
    public FormulaError(String reason)
        : this()
    {
      Reason = reason;
    }

    /// <summary>
    ///  The reason why this FormulaError was created.
    /// </summary>
    public string Reason { get; private set; }
  }
    /// <summary>
    /// Extensions for the Stack Object. Deals with checking all the operators.
    /// Made particularly for this instance. 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Checks if stack has + or - on top.
        /// </summary>
        /// <param name="stack">object calling the method</param>
        /// <param name="s">string to be tested</param>
        /// <returns>true or false</returns>
        public static Boolean HasOnTopSumOrDifference(this Stack<string> stack, string s)
        {
            if (s.Equals("+") || s.Equals("-"))
                return true; 
               
            return false;
        }
        /// <summary>
        /// Checks if stack has / or * on top. 
        /// </summary>
        /// <param name="stack">object calling the method</param>
        /// <param name="s">string to be tested</param>
        /// <returns>true or false</returns>
        public static Boolean HasOnTopMultOrDiv(this Stack<string> stack, string s)
        {
            if (s.Equals("*") || s.Equals("/"))
                return true;

            return false;
        }
        
    }
}

