// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary> 
/// Author:    Alejandro Serrano (u1214728)
/// Partner:   None
/// Date:      January 26, 2020 
/// Course:    CS 3500, University of Utah, School of Computing 
///  
/// I, Alejandro Serrano (u1214728), aside from the starter code given to us, 
/// certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 

namespace SpreadsheetUtilities
{

  /// <summary>
  /// (s1,t1) is an ordered pair of strings
  /// t1 depends on s1; s1 must be evaluated before t1
  /// 
  /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
  /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
  /// Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
  /// set, and the element is already in the set, the set remains unchanged.
  /// 
  /// Given a DependencyGraph DG:
  /// 
  ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
  ///        (The set of things that depend on s)    
  ///        
  ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
  ///        (The set of things that s depends on) 
  //
  // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
  //     dependents("a") = {"b", "c"}
  //     dependents("b") = {"d"}
  //     dependents("c") = {}
  //     dependents("d") = {"d"}
  //     dependees("a") = {}
  //     dependees("b") = {"a"}
  //     dependees("c") = {"a"}
  //     dependees("d") = {"b", "d"}
  /// </summary>
  public class DependencyGraph
  {
        /*
             * The dependents dictionary will hold the corresponding
             * dependents and it's dependees in a HashSet of strings
             */
        private Dictionary<string, HashSet<string>> dependents;
        /*
         * The dependees dictionary will hold the corresponding
         * dependees and it's dependents in a HashSet of strings
         */
        private Dictionary<string, HashSet<string>> dependees;
        /*
         * Private int variable that will update the amount of
         * pairs (s, t) dependents holds. 
         */
        private int size_Of_Dependents;

        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        public DependencyGraph()
        {

            dependents = new Dictionary<string, HashSet<string>>();
            dependees = new Dictionary<string, HashSet<string>>();
            size_Of_Dependents = 0;
        }


        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return this.size_Of_Dependents; }
        }


        /// <summary>
        /// The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("a")
        /// </summary>
        public int this[string s]
        {
            get { return dependees[s].Count; }
        }


        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            if (dependents.ContainsKey(s))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            if (dependees.ContainsKey(s))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            if (dependents.ContainsKey(s))
            {
                foreach (string u in dependents[s])
                {
                    yield return u;
                }
            }
        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            if (dependees.ContainsKey(s))
            {
                foreach (string u in dependees[s])
                {
                    yield return u;
                }
            }


        }

        /// <summary>
        /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
        /// 
        /// <para>This should be thought of as:</para>   
        /// 
        ///   t depends on s
        ///
        /// </summary>
        /// <param name="s"> s must be evaluated first. T depends on S</param>
        /// <param name="t"> t cannot be evaluated until s is</param>        /// 
        public void AddDependency(string s, string t)

        // We maintain a balance between the 
        // Dependents Dictionary and Dependees Dictionary
        {
            if (dependents.ContainsKey(s))
            {
                dependents[s].Add(t);
                size_Of_Dependents++;
            }
            else
            {
                dependents.Add(s, new HashSet<string>());
                dependents[s].Add(t);
                size_Of_Dependents++;
            }

            if (dependees.ContainsKey(t))
            {
                dependees[t].Add(s);

            }
            else
            {
                dependees.Add(t, new HashSet<string>());
                dependees[t].Add(s);
            }
        }


        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void RemoveDependency(string s, string t)
        {
            if (dependents.ContainsKey(s))
            {
                // Dependending on the size of the hash of dependees, 
                // we either simply remove it from the hash set, or
                // we delete the entire value from dependents. 
                if (dependents[s].Contains(t) && dependents[s].Count > 1)
                {
                    dependents[s].Remove(t);
                }

                else if (dependents[s].Contains(t) && dependents[s].Count <= 1)
                {
                    HashSet<string> hashToBeRemoved = new HashSet<string>();
                    dependents.Remove(s, out hashToBeRemoved);
                }

                size_Of_Dependents--;
            }
            if (dependees.ContainsKey(t))
            {
                dependees[t].Remove(s);
            }
        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            if (dependents.ContainsKey(s))
            {
                // Based on the dependent, we visit each of its dependees
                // and we delete the corresponding dependent from the HashSet.
                foreach (string dependee_To_Be_Searched in this.dependents[s])
                {
                    this.dependees[dependee_To_Be_Searched].Remove(s);
                }
                size_Of_Dependents = size_Of_Dependents - dependents[s].Count;
                dependents[s].Clear();
            }
            else
            {
                dependents.Add(s, new HashSet<string>());
            }
            foreach (string dependent in newDependents)
            {
                dependents[s].Add(dependent);
                size_Of_Dependents++;
                ReplaceDependees(dependent, s);
            }
        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            if (dependees.ContainsKey(s))
            {
                foreach (string b in this.dependees[s])
                {
                    this.dependents[b].Remove(s);
                }
                dependees[s].Clear();
            }
            else
            {
                dependees.Add(s, new HashSet<string>());
            }


            foreach (string dependee in newDependees)
            {

                dependees[s].Add(dependee);
                ReplaceDependents(dependee, s);
            }
        }
        /// <summary>
        /// Private helper method for the ReplaceDependents public method.
        /// Handles the dependees by creating each individual one from the 
        /// HashSet that is passed into the public method. 
        /// </summary>
        /// <param name="s">dependee</param>
        /// <param name="t">dependent</param>
        private void ReplaceDependees(string s, string t)
        {
            if (dependees.ContainsKey(s))
            {

                dependees[s].Add(t);
                return;
            }
            else
            {
                dependees.Add(s, new HashSet<string> { t });
            }
        }
        /// <summary>
        /// Private helper method for the ReplaceDependees public method.
        /// Handles the dependents by creating each individual one from the 
        /// HashSet that is passed into the public method. 
        /// </summary>
        /// <param name="s">dependent</param>
        /// <param name="t">dependee</param>
        private void ReplaceDependents(string s, string t)
        {
            if (dependents.ContainsKey(s))
            {

                dependents[s].Add(t);
                return;
            }
            else
            {
                dependents.Add(s, new HashSet<string> { t });
            }
        }

    }

}
