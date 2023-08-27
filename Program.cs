using System;
using RecipeManagementSystem.Models;
using RecipeManagementSystem.Services;
using RecipeManagementSystem.Utils;

namespace RecipeManagementSystem
{
    class Program
    {
        private static List<HistoryEntry> historyEntries = new List<HistoryEntry>();

        static void Main(string[] args)
        {
            RecipeService recipeService = new RecipeService();
            List<Recipe> recipes = new List<Recipe>(); // Declare recipes variable here

            while (true)
            {
                Console.Clear();
                ConsoleUtils.DisplayHeader("Recipe Management System");
                Console.WriteLine("1. Add Recipe");
                Console.WriteLine("2. View Recipes");
                Console.WriteLine("3. Update Recipe");
                Console.WriteLine("4. Delete Recipe");
                Console.WriteLine("5. View History");
                Console.WriteLine("6. Exit");

                int choice = ConsoleUtils.GetChoice(1, 6);


                switch (choice)
                {
                    case 1:
                        Console.Write("Enter recipe name: ");
                        string recipeName = Console.ReadLine();
                        recipeService.AddRecipe(recipeName);
                        historyEntries.Add(new HistoryEntry
                        {
                            CommandName = "Add Recipe",
                            Timestamp = DateTime.Now,
                            Recipes = new List<string> { recipeName }
                        });
                        Console.WriteLine($"Recipe '{recipeName}' added successfully!"); // Display message with the added recipe name


                        break;

                    case 2:
                        Console.Clear();
                        ConsoleUtils.DisplayHeader("View Recipes");

                        recipes = recipeService.GetRecipes(); // Fetch the recipes for display


                        if (recipes.Count > 0)
                        {
                            Console.WriteLine("Recipes:");
                            for (int i = 0; i < recipes.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No recipes found.");
                        }
                        HistoryEntry viewRecipesEntry = new HistoryEntry
                        {
                            CommandName = "View Recipes",
                            Timestamp = DateTime.Now,
                            Recipes = recipes.Select(recipe => recipe.Name).ToList()
                        };
                        historyEntries.Add(viewRecipesEntry);

                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Clear();
                        ConsoleUtils.DisplayHeader("Update Recipe");

                        recipes = recipeService.GetRecipes();

                        if (recipes.Count > 0)
                        {
                            Console.WriteLine("Select a recipe to update:");
                            for (int i = 0; i < recipes.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
                            }

                            int updateIndex = ConsoleUtils.GetPositiveInt("Enter the index of the recipe to update: ");

                            if (updateIndex >= 1 && updateIndex <= recipes.Count)
                            {
                                Console.Write("Enter the new name for the recipe: ");
                                string newName = Console.ReadLine();

                                Recipe recipeToUpdate = recipes[updateIndex - 1];
                                string originalName = recipeToUpdate.Name;
                                recipeService.UpdateRecipe(recipeToUpdate, newName); // Use the correct method
                                historyEntries.Add(new HistoryEntry
                                {
                                    CommandName = "Update Recipe",
                                    Timestamp = DateTime.Now,
                                    Recipes = new List<string> { $"{originalName} has been replaced with {newName}" }
                                });

                                Console.WriteLine($"Recipe '{originalName}' updated to '{newName}'!");

                            }
                            else
                            {
                                Console.WriteLine("Invalid index.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No recipes found.");
                        }

                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;



                    case 4:
                        Console.Clear();
                        ConsoleUtils.DisplayHeader("Delete Recipe");
                        Console.WriteLine("Select a recipe to delete:");

                        recipes = recipeService.GetRecipes(); // Fetch the recipes for display
                        for (int i = 0; i < recipes.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {recipes[i].Name}");
                        }

                        Console.Write("Enter the index of the recipe to delete: ");
                        int deleteIndex = ConsoleUtils.GetPositiveInt("Please enter a valid index: ");

                        if (deleteIndex >= 1 && deleteIndex <= recipes.Count)
                        {
                            string deletedRecipeName = recipes[deleteIndex - 1].Name;
                            Console.WriteLine($"Recipe to delete: {deletedRecipeName}");
                            Console.Write("Are you sure you want to delete this recipe? (Y/N): ");
                            string confirmation = Console.ReadLine().Trim().ToLower();

                            if (confirmation == "y" || confirmation == "yes")
                            {
                                recipeService.RemoveRecipe(deletedRecipeName); // Delete the recipe from the service
                                historyEntries.Add(new HistoryEntry
                                {
                                    CommandName = "Delete Recipe",
                                    Timestamp = DateTime.Now,
                                    Recipes = new List<string> { deletedRecipeName }
                                });
                                Console.WriteLine($"Recipe '{deletedRecipeName}' deleted!");

                            }
                            else
                            {
                                Console.WriteLine("Deletion canceled.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid index.");
                        }

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case 5: // History Command
                        Console.Clear();
                        ConsoleUtils.DisplayHeader("Command History");

                        foreach (var entry in historyEntries)
                        {
                            Console.WriteLine($"{entry.Timestamp}: {entry.CommandName}");
                            if (entry.CommandName == "View Recipes")
                            {
                                Console.WriteLine("Recipes at that moment:");
                                foreach (var recipe in entry.Recipes)
                                {
                                    Console.WriteLine(recipe);
                                }
                            }
                        }

                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case 6:
                        Environment.Exit(0);
                        break;
                }
                Console.Clear();
            }
            Console.Clear();
        }
    }
}