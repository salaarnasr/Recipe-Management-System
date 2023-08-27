using System;
using RecipeManagementSystem.Models;
using RecipeManagementSystem.Services;
using RecipeManagementSystem.Utils;

namespace RecipeManagementSystem
{
    class Program
    {
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
                Console.WriteLine("5. Exit");

                int choice = ConsoleUtils.GetChoice(1, 5);


                switch (choice)
                {
                    case 1:
                        Console.Write("Enter recipe name: ");
                        string recipeName = Console.ReadLine();
                        recipeService.AddRecipe(recipeName);
                        Console.WriteLine($"Recipe '{recipeName}' added successfully!"); // Display message with the added recipe name
                        break;

                    case 2:
                        Console.Clear();
                        ConsoleUtils.DisplayHeader("View Recipes");

                        recipes = recipeService.GetRecipes();

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
                                recipeService.UpdateRecipe(recipeToUpdate, newName); // Use the newly added method

                                Console.WriteLine($"Recipe '{recipeToUpdate.Name}' updated to '{newName}'!");
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

                        // Get the updated list of recipes
                        recipes = recipeService.GetRecipes();

                        // Display the list of recipes
                        Console.WriteLine("Select a recipe to delete:");
                        for (int i = 0; i < recipes.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {recipes[i].Name}");
                        }

                        // Prompt for the index of the recipe to delete
                        int deleteIndex = ConsoleUtils.GetPositiveInt("Enter the index of the recipe to delete: ");

                        if (deleteIndex >= 1 && deleteIndex <= recipes.Count)
                        {
                            string deletedRecipeName = recipes[deleteIndex - 1].Name;
                            Console.WriteLine($"Recipe to delete: {deletedRecipeName}");
                            Console.Write("Are you sure you want to delete this recipe? (Y/N): ");
                            string confirmation = Console.ReadLine().Trim().ToLower();

                            if (confirmation == "y" || confirmation == "yes")
                            {
                                recipes.RemoveAt(deleteIndex - 1);
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


                    case 5:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
