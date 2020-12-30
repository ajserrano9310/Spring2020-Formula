Author:     Alejandro Serrano
Partner:    None
Date:       January 17, 2020
Course:     CS 3500, University of Utah, School of Computing
Assignment: Assignment #1 - FormulaEvaluator
Copyright:  CS 3500 and Alejandro Serrano - This work may not be copied for use in Academic Coursework.

1. Comments to Evaluators:

Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam remaperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.

The current assignment was expected to take around 8 hours. Nonetheless, the estimation was larger. The assignment took around 11 - 13 hours to complete.

2. Assignment Specific Topics
The current assignment is part of a larger piece of software: A Spreadsheet. Currently, this software simply evaluates infix expressions with certain established variables included on the Tester code. 
This software will be used to evaluate the infix expressions inside the Spreadsheet. 
Some of the more difficult test cases were shared by my classmates in order to check for correctness. 

3. Consulted Peers:

-Alejandro Rubio 
-Aaron Morgan
-Tyler Gordon

4. References:

    1. https://www.geeksforgeeks.org/ - https://www.geeksforgeeks.org/c-sharp-stack-class/	
    2. https://www.geeksforgeeks.org/ - https://www.geeksforgeeks.org/what-is-regular-expression-in-c-sharp/
    3. https://www.geeksforgeeks.org/ - https://www.geeksforgeeks.org/c-sharp-trim-method/
    4. https://docs.microsoft.com/ - https://docs.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=netframework-4.8
    5. https://docs.microsoft.com/ - https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1?view=netframework-4.8
    6. https://docs.microsoft.com/ - https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception?view=netframework-4.8

Author:     Alejandro Serrano
Partner:    None
Date:       January 26, 2020
Course:     CS 3500, University of Utah, School of Computing
Assignment: Assignment #2 - DependencyGraph
Copyright:  CS 3500 and Alejandro Serrano - This work may not be copied for use in Academic Coursework.

1. Comments to Evaluators:

Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam remaperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.

The analysis took around 2 hours; implementing such analysis took around 4 hours; testing and developing correct Unit Tests around 5 hours. Total of 11 hours. 
One of the struggles that I faced was figuring out exactly what would happen to the dependee by adding a new dependency through the ReplaceDependents method with
and empty HashSet. Considering there is no dependee to add, nothing is done with the dependee table, and the dependent dictionary will just have a key with an empty set. 

2. Assignment Specific Topics

The current assignment deals with the dependency of what will be the cells in the Spreadsheet. Therefore, I make use of two dictionaries: dependents and dependees. They are
represented as follows:
If we add a dependency in the form of ("a", "b"):
    Dependents ("a") = {"b"}
    Dependees ("b") = {"a"}
Therefore, a relationship is created between dependent and dependee. 

3. Consulted Peers:
-Aaron Morgan: We consulted regarding which data structure was more appropriate to use for this assignment. Just as well, several testing strategies that could help
test the correcteness of the code. 


4. References:

    1. https://docs.microsoft.com/ - https://docs.microsoft.com/en-us/dotnet/api/system.collections.hashtable?view=netframework-4.8
    2. https://docs.microsoft.com/ - https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=netframework-4.8
    3. https://www.geeksforgeeks.org/ - https://www.geeksforgeeks.org/c-sharp-hashset-class/
    4. https://www.dotnetperls.com/ - https://www.dotnetperls.com/ienumerable

Author:     Alejandro Serrano
Partner:    None
Date:       January 31, 2020
Course:     CS 3500, University of Utah, School of Computing
Assignment: Assignment #3 - Formula
Copyright:  CS 3500 and Alejandro Serrano - This work may not be copied for use in Academic Coursework.

1. Comments to Evaluators:

Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, 
totam remaperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. 
Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos 
qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, 
adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.

The analysis took around 2 hours; implementing such analysis took around 6 hours; 
testing and developing correct Unit Tests around 6 hours. Total of 14 hours.

The main issues that were faced for this assignment were:
    1. Understanding the two constructors
    2. Understanding the relationship between Equals and HashCode and if 
       we needed to check the values even if the HashCode result was the same. 
    3. Developing the parsing rules
    4. Developing the extensions for Good Software Practice

2. Assignment Specific Topics

The current assignment deals with the syntax of the formula we were dealing with during 
the first Assignment. Nonetheless, now we take care of it with the constructor and check
beforehand if the formula can potentially be evaluated. 

3. Consulted Peers:
-Aaron Morgan: We talked about the analysis and how the assignment worked in general. 
-Tyler Gordon: The difference between constructors. 
-Alejandro Rubio: Discussion regarding Formula Error and edge cases. 

4. References
    1. https://docs.microsoft.com - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
    2. https://docs.microsoft.com - https://docs.microsoft.com/en-us/dotnet/api/system.double.parse?view=netframework-4.8