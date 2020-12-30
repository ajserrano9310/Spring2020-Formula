Author:     Alejandro Serrano
Partner:    None
Date:       January 31, 2020
Course:     CS 3500, University of Utah, School of Computing
Assignment: Assignment #3 - Formula
Copyright:  CS 3500 and Alejandro Serrano - This work may not be copied for use in Academic Coursework.

1. Comments to Evaluators:

Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam remaperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.

The analysis took around 2 hours; implementing such analysis took around 6 hours; 
testing and developing correct Unit Tests around 6 hours. Total of 14 hours.

The main issues that were faced for this assignment were:
    1. Understanding the two constructors
    2. Understanding the relationship between Equals and HashCode and if 
       we needed to check the values even if the HashCode result was the same. 
    3. Developing the parsing rules
    4. Developing the extensions for Good Software Practice

It's also important to mention that some of the tests for Evaluate were used previously
for A1. Only those that relate to more complicated infix notation. 
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