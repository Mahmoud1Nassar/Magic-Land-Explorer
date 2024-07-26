# Magic Land Explorer

Magic Land Explorer is a C# console application that reads JSON data about various destinations in Magic Land and provides an interactive interface to filter, sort, and display information about these destinations.

## Project Structure

- **Category.cs**: Defines the `Category` class with properties for `CategoryName` and a list of `Destination` objects.
- **Destination.cs**: Defines the `Destination` class with properties for `Name`, `Type`, `Location`, `Duration`, `Description`, and `ShowTime`.
- **Program.cs**: The main entry point of the application, which reads JSON data, performs LINQ queries, and provides an interactive console interface.

## Features

- Read JSON data from a file and parse it into C# objects.
- Perform data manipulation and filtering using LINQ queries and Lambda expressions.
- Interactive console interface for user interaction.
- Delegates to handle user interactions and display information.
- Error handling for invalid user inputs and missing data.