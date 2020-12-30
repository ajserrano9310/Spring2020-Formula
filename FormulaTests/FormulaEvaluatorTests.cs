using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SpreadsheetUtilities;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
/// Author:    Alejandro Serrano (u1214728)
/// Partner:   None
/// Date:      January 31, 2020 
/// Course:    CS 3500, University of Utah, School of Computing 
///  
/// I, Alejandro Serrano (u1214728), certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README f
namespace FormulaTests
{
    /// <summary>
    /// Deals with testing the Formula Syntax and some Infix Expressions
    /// </summary>
    [TestClass]
    public class FormulaEvaluatorTests
    {
        /// <summary>
        /// When called normalizes the variable from a1 to A1 when
        /// the constructor with parameters is called
        /// </summary>
        Func<string, string> normalize = delegate (string s)
        {
            return s.ToUpper();
        };
        /// <summary>
        /// 
        /// Checks to see if the pattern for the variables is valid
        /// </summary>
        Func<string, bool> isValid = delegate (string s)
        {

            if((Regex.IsMatch(s, "^[a-zA-Z]+[0-9]+")))
            {
                return true;
            }

            throw new FormulaFormatException("Variable is not valid");
            
        };
        /// <summary>
        /// Evaluates the corresponding variable into it's respective double
        /// </summary>
        Func<string, double> lookup = delegate (string s)
        {
            if (s.Equals("A1"))
            {
                return 2.0;
            }
            if (s.Equals("A2"))
            {
                return 2.5;
            }
            if (s.Equals("A3"))
            {
                return 3.24;
            }
            if (s.Equals("A4"))
            {
                return 4.0;
            }

            throw new FormulaFormatException("Not valid Formula");
        };

        // CONSTRUCTOR TESTS
        /// <summary>
        /// Simple test for constructor with delegates
        /// </summary>
        [TestMethod]
        public void TestForSimpleExpressionWithDelegates()
        {
            Formula f = new Formula("x2+y2", normalize, isValid);
            Assert.AreEqual("X2+Y2", f.ToString());
            
        }
        /// <summary>
        /// Simple test for constructor with no delegates
        /// </summary>
        [TestMethod()]
        public void TestForSimpleExpressionNoDelegates()
        {
            Formula f = new Formula("y2 + z4");
            Assert.AreEqual("y2+z4", f.ToString());

        }
        /// <summary>
        /// Null formula
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException),
            "The Formula was null.")]
        public void FormulaAsNull()
        {
            Formula f = new Formula(null);
        }
        /// <summary>
        /// Testing passing variables to the constructor
        /// </summary>
        [TestMethod()]
        public void TestForExpressionWithValidVariable()
        {
            Formula f = new Formula("x2+2", normalize, isValid);
            Assert.AreEqual("X2+2", f.ToString());
        }
        /// <summary>
        /// Not valid variable format
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException),
        "The variable is not allowed")]
        public void TestForInvalidVariable()
        {
            Formula f = new Formula("xx+2", normalize, isValid);
        }
        /// <summary>
        /// Wrong last final value
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException),
       "The variable is not allowed")]
        public void TestForExtraOperator()
        {
            Formula f = new Formula("X2++", normalize, isValid);
        }
        /// <summary>
        /// Tests for correct amount of parenthesis
        /// </summary>
        [TestMethod()]
        public void TestValidFormulaParenthesis()
        {
            Formula f = new Formula("(2+2)", normalize, isValid);

            Assert.AreEqual("(2+2)", f.ToString());
            Assert.AreEqual(4.0, f.Evaluate(lookup));
        }
        /// <summary>
        /// Tests for invalid amount of parenthesis
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException),
        "The variable is not allowed")]
        public void TestInvalidAmountOfParentheses()
        {
            Formula f = new Formula("(2+1", normalize, isValid);
        }
        /// <summary>
        /// one side more than the other
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException),
        "Parentheses failure")]
        public void TestingMoreRightParenThanLeft()
        {
            Formula f = new Formula("2+1)", normalize, isValid);
        }
        /// <summary>
        /// empty formula
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException),
        "The Expression is empty")]
        public void OneTokenRule()
        {
            Formula f = new Formula(" ", normalize, isValid);
        }
        /// <summary>
        /// Invalid token at the beginning of formula
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException),
       "Incorrect Formula Format")]
        public void IncorretStartingToken()
        {
            Formula f = new Formula("# + 1", normalize, isValid);
        }
        /// <summary>
        /// Invalid last token
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException),
       "Incorrect Formula Format")]
        public void IncorretLastToken()
        {
            Formula f = new Formula("1 + #", normalize, isValid);
        }
        /// <summary>
        /// After potential variable there should be operator
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException),
       "Incorrect Formula Format")]
        public void IncorretVariableWithSpaceType()
        {
            Formula f = new Formula("x 2 + 2", normalize, isValid);
        }
        /// <summary>
        /// Testing parsing scientific notation
        /// </summary>
        [TestMethod()]
        public void TestingScientificNotation()
        {
            Formula f = new Formula("1e-3 + X2", normalize, isValid);
            Assert.AreEqual(5.001, f.Evaluate(s => 5));
        }
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException),
       "Incorrect Formula Format")]
        public void TestingFollowingRule()
        {
            Formula f = new Formula("2++1");
        }

        // EVALUATE TESTS
        /// <summary>
        /// Simple expression with delegates
        /// </summary>
        [TestMethod()]
        public void TestEvaluateForSimpleExpression()
        {
            Formula f = new Formula("2+2", normalize, isValid);
           
            Assert.AreEqual(4.0, f.Evaluate(lookup));
        }
        /// <summary>
        /// No Delegates
        /// </summary>
        [TestMethod()]
        public void TestEvaluateWithNoDelegateConstructor()
        {
            Formula f = new Formula("2+2");
            Assert.AreEqual(4.0, f.Evaluate(lookup));
        }
        /// <summary>
        /// Testing double sum and lambda
        /// </summary>
        [TestMethod()]
        public void TestEvaluateWithVariable()
        {
            Formula f = new Formula("x2+2.5", normalize, isValid);
            Assert.AreEqual(8.5, f.Evaluate(s => 6.0));
        }
        /// <summary>
        /// Decimal values
        /// </summary>
        [TestMethod()]
        
        public void TestEvaluateWithTwoDecimalValues()
        {
            Formula f = new Formula("2.5+2.5", normalize, isValid);
            Assert.AreEqual(5.0, f.Evaluate(lookup));
        }
        [TestMethod()]
        public void TestEvaluateWithDecimalsAndVariable()
        {
            Formula f = new Formula("2.25 + A3", normalize, isValid);
            Assert.AreEqual(5.49, f.Evaluate(lookup));
        }

        /// <summary>
        /// Simple sum
        /// </summary>
        [TestMethod()]
        public void TestingEvaluateWithSimpleExpressionSum()
        {
            Formula f = new Formula("1+1+1");
            Assert.AreEqual(3.0, f.Evaluate(lookup));
        }
        /// <summary>
        /// simple substraction 
        /// </summary>
        [TestMethod()]

        public void TestingEvaluateWithSimpleExpressionSub()
        {
            Formula f = new Formula("1+1-1");
            Assert.AreEqual(1.0, f.Evaluate(lookup));
        }
        /// <summary>
        /// Mainly testing order of prescedence
        /// </summary>
        [TestMethod()]
        public void TestingEvaluateWithSimpleExpressionMult()
        {
            Formula f = new Formula("1+1*1");
            Assert.AreEqual(2.0, f.Evaluate(lookup));
        }
        /// <summary>
        /// Testing lambda and the single variable expression
        /// </summary>
        [TestMethod()]
        public void TestingWithSingleValue()
        {
            Formula f = new Formula("X2", normalize, isValid);
            Assert.AreEqual(2.0, f.Evaluate(s => 2.0));
        }

        [TestMethod()]
        public void TestingEvaluateWithSimpleExpressionDiv()
        {
            Formula f = new Formula("1+1/1");
            Assert.AreEqual(2.0, f.Evaluate(lookup));
        }
        /// <summary>
        /// Division by zero
        /// </summary>
        [TestMethod()]
        
        public void TestingEvaluateWithDivisionByZero()
        {
            
            Formula f = new Formula("1/0");
           
            Assert.IsTrue(f.Evaluate(lookup)is FormulaError);
        }

        // Test Get Variables
        /// <summary>
        /// Goes over IEnumarable to check variables
        /// </summary>
        [TestMethod()]
        public void TestingGetVariablesWithDelegateConstructor()
        {
            Formula f = new Formula("x2 + y3 + z1", normalize, isValid);
            IEnumerator<string> e = f.GetVariables().GetEnumerator();

            Assert.IsTrue(e.MoveNext());
            string s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            string s2 = e.Current;
            Assert.IsTrue(e.MoveNext());
            string s3 = e.Current;
            Assert.IsFalse(e.MoveNext());

            Assert.AreEqual("X2", s1);
            Assert.AreEqual("Y3", s2);
            Assert.AreEqual("Z1", s3);

        }
        [TestMethod()]
        public void TestingGetVariablesWithRepeatedVariable()
        {
            Formula f = new Formula("X2 + Y3 + Y3", normalize, isValid);
            IEnumerator<string> e = f.GetVariables().GetEnumerator();

            Assert.IsTrue(e.MoveNext());
            string s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            string s2 = e.Current;
            Assert.IsFalse(e.MoveNext());

            Assert.AreEqual("X2", s1);
            Assert.AreEqual("Y3", s2);
            

        }

        // Test Equals
        /// <summary>
        /// Tests equality with both Equals and operators
        /// </summary>
        [TestMethod()]
        public void TestEquals()
        {
            Formula f = new Formula("x2+y2", normalize, isValid);
            Assert.IsTrue(f.Equals(new Formula("X2+Y2")));
            Formula f2 = new Formula("X2+Y2");
            Assert.AreEqual(true, f == f2);
        }

        [TestMethod()]
        public void TestEqualsWithNoDelegateConstructor()
        {
            Formula f = new Formula("x2+y2");
            Assert.IsFalse(f.Equals(new Formula("X2+Y2")));
            Formula f2 = new Formula("X2+Y2");
            Assert.IsTrue(f != f2);
        }
        /// <summary>
        /// Testing parsing doubles for equality
        /// </summary>
        [TestMethod()]
        public void TestEqualsWithDoubleExpression()
        {
            Formula f = new Formula("2.5 + 2.5", normalize, isValid);
            Formula f2 = new Formula("2.5 + 2.500");
            Assert.IsTrue(f.Equals(f2));
        }
        /// <summary>
        /// Testing equality with scientific notation
        /// </summary>
        [TestMethod()]
        public void TestEqualsWithScientificNotation()
        {
            Formula f = new Formula("1e-3 + 2.5", normalize, isValid);
            Formula f2 = new Formula("0.001 + 2.5");
            Assert.IsTrue(f.Equals(f2));
        }
        [TestMethod()]
        public void TestEqualsWithScientificNotation2()
        {
            Formula f = new Formula("0.001 + 2.5", normalize, isValid);
            Formula f2 = new Formula("1e-3 + 2.5");
            Assert.IsTrue(f.Equals(f2));
        }
        /// <summary>
        /// Testing Inequality with confusing values
        /// </summary>
        [TestMethod()]
        public void TestEqualsForDifferentValues()
        {
            Formula f = new Formula("2.5 + 2.5");
            Formula f2 = new Formula("2.5 + 2.501");
            Assert.IsFalse(f.Equals(f2));

        }
        /// <summary>
        /// Testing inequality with different variables
        /// </summary>
        [TestMethod()]
        public void TestEqualsForDifferentVariables()
        {
            Formula f = new Formula("2.5 + X2");
            Formula f2 = new Formula("2.5 + X1");
            Assert.IsFalse(f.Equals(f2));
        }
        /// <summary>
        /// Different Objects
        /// </summary>
        [TestMethod()]
        public void TestEqualsForDifferentObject()
        {
            Formula f = new Formula("2.5 + X2");
            string s = "let's hope is false";
            Assert.IsFalse(f.Equals(s));
        }
        /// <summary>
        /// Object is null
        /// </summary>
        [TestMethod()]
        public void TestEqualsWithObjectNull()
        {
            Formula f = new Formula("2");
            Formula f2 = null;
            Assert.IsFalse(f.Equals(f2));
        }
        /// <summary>
        /// Functionality of equal operators
        /// </summary>
        [TestMethod()]
        public void TestEqualityWithNullOperator()
        {
            Formula f1 = null;
            Formula f2 = null;
            Assert.IsTrue(f1 == f2);
        }

        [TestMethod()]
        public void TestInEqualityWithNullOperator()
        {
            Formula f1 = null;
            Formula f2 = new Formula("x2+1");
            Assert.IsTrue(f1 != f2);
        }

        // From here we evaluate complicated infix expressions
        // similar to the ones used for A1. They will be mostly from
        // the FormulaEvaluator Tester

        // TestEvaluate 1_0 to 1_4 will test simple no decimal expressions

        [TestMethod()]
        public void TestEvaluate1_0()
        {
            Formula f = new Formula("(30*2) - (4*(4+1))/2*2");
            Assert.AreEqual(40.0, f.Evaluate(lookup));
        }

        [TestMethod()]
        public void TestEvaluate1_1()
        {
            Formula f = new Formula("(30*2) - 4*(4+1)/2*2");
            Assert.AreEqual(40.0, f.Evaluate(lookup));
        }

        [TestMethod()]
        public void TestEvaluate1_2()
        {
            Formula f = new Formula("100*(25/5)+30/6+10");
            Assert.AreEqual(515.0, f.Evaluate(lookup));
        }

        [TestMethod()]
        public void TestEvaluate1_3()
        {
            Formula f = new Formula("(5*(3+2))");
            Assert.AreEqual(25.0, f.Evaluate(lookup));
        }
        [TestMethod()]
        public void TestEvaluate1_4()
        {
            Formula f = new Formula("20/2+1");
            Assert.AreEqual(11.0, f.Evaluate(lookup));
        }

        // From here we evaluate infix expressions with variables
        // both using Lambda expressions and Lookup Function
        // similar to the ones used for A1. 

        // TestEvaluate 2_0 to 2_4. 
    }
}
