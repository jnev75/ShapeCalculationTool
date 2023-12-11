# C# Shape Calculation Tool Console Application

**Version 1.0**

This is a sample readme file for my GitHub repo. I'm using Markdown

* I'm developing an Object Oriented Console Application
* Using C# version 8.0 or later

Authorship documentation for the newly created project

## Any known bugs

---

## Update log

---

## Contributors

- James L Neville

---

## License & copyright

© James L Neville, C# Shape Calculation Tool Console Application

---

## Terms of use

By using this console application, you acknowledge that the content developed is subject to copyright enforcement.

This intellectual property right grants that the application may be used solely for personal use, as a reference, or for intended research purposes.

This policy also implies that the application cannot be modified, duplicated, re-distributed to a third party, or exploited for commercial use.

Any illegitimate use of the application will be considered a breach of the terms of use agreement.

This may further violate copyright and trademark laws which are bound by the Copyright, Designs and Patents Act 1988.

---

## Operations

The shape calculation tool console application can be used to calculate the area and boundary length of a given shape.


To begin testing this program, the user must firstly open the solution (.sln) file named 'ShapeCalculationTool'.

This can be done using a legitimate and interoperable Text Editor such as Visual Studio Code. Or an IDE such as Visual Studio 2022 Community edition.

Next, the user must open the C# (.cs) file named ShapeMenu from within the ShapeCalculationTool project folder.

Before the user starts debugging the program, it is necessary to comment out the throw statements within the catch blocks of this file.

This will ensure program execution runs smoothly; that any exceptions are handled gracefully.


Now, the user can run the program, by selecting the green arrow button with 'ShapeCalculationTool' as description from the Toolbar.

Alternatively, it is possible to run the program by accessing 'Start Debugging' under Debug on the Menu Bar. Or by pressing the F5 button on a keyboard.

The console window should now open, the menu header should be displayed, as well as relevant shape menu dialogue and a prompt requesting shape input.


For the first prompt, the user is expected to enter a valid letter character as shape input.

Once the user provides shape input and presses the Enter button, the data will undergo validation.

This will ensure that it fully meets the criteria required.

Only the letter characters 's', 't' or 'c' are accepted as valid data.

Should the user happen to enter any leading and/or trailing white-space characters alongside the valid data, these will be removed from the string.

In the event that invalid data is entered, the user will receive an error message and an instruction (hint).

The shape menu dialogue will be printed to the console once again, followed by the same prompt for them to retry.

Shape Character Data that will be considered invalid include the following:

- null (nothing entered)
- empty string
- any lowercase letter that isn't: (s/t/c)
- any uppercase letter that is or isn't: (S/T/C)
- more than one letter (even if valid)
- any symbol, for example: $
- any negative or positive number or zero


For the second prompt, the user is expected to enter a valid integer as length input.

Once the user provides length input and presses the Enter button, the data will undergo validation.

This will ensure that it fully meets the criteria required.

Only positive integer numbers between 1 and 999 are accepted as valid data.

Should the user happen to enter any leading and/or trailing white-space characters alongside the valid data, these will be removed from the string.

In the event that invalid data is entered, the user will receive an error message and an instruction (hint).

The second prompt will be printed to the console for them to retry.

Length Integer Data that will be considered invalid include the following:

- null (nothing entered)
- empty string
- any lowercase or uppercase letter
- any symbol, for example: $
- any integer number less than 1 or greater than 999


Should the user provide valid data for both prompts, the shape calculation results will now be displayed.

Here is an example of valid user input, followed by the results from both calculations:

shape: s
length: 5

****************************************************************************************

Shape Calculation Results:

Shape: Square
Length: 5 cm
Area: 25 cm²
Boundary Length: 20 cm

****************************************************************************************


Finally, response menu dialogue will be displayed, followed by a third prompt.

At this stage, the user is asked if they would like to calculate area and boundary length metrics for another shape.

For the third prompt, the user is expected to enter a valid letter character as reponse input.

Once the user provides response input and presses the Enter button, the data will undergo validation.

This will ensure that it fully meets the criteria required.

Only the letter characters 'y' or 'n' are accepted as valid data.

Should the user happen to enter any leading and/or trailing white-space characters alongside the valid data, these will be removed from the string.

In the event that invalid data is entered, the user will receive an error message and an instruction (hint).

The response menu dialogue will be printed to the console once again, followed by the same prompt for them to retry.

Response Character Data that will be considered invalid include the following:

- null (nothing entered)
- empty string
- any lowercase letter that isn't: (y/n)
- any uppercase letter that is or isn't: (Y/N)
- more than one letter (even if valid)
- any symbol, for example: $
- any negative or positive number or zero

When the user enters the letter character 'y' and presses Enter, they can then proceed to make calculations for another shape.

Likewise, the menu header should be displayed, as well as relevant shape menu dialogue and a prompt requesting shape input.

However, if a reponse of 'n' is entered, the user can then press any key to exit the program.

---

## Testing Exceptions

This project includes `throw` statements within catch blocks for testing purposes.

These statements are commented out by default to showcase the clean execution of the program.

If you want to test exception handling, uncomment the relevant lines in the code.