using System;
using System.Collections.Generic;
using RecipeManagementSystem.Models;

namespace RecipeManagementSystem.Services
{
    public class RecipeService
    {
        private List<Recipe> recipes;

        public RecipeService()
        {
            recipes = new List<Recipe>();
        }

        public void AddRecipe(string recipeName)
        {
            Recipe newRecipe = new Recipe { Name = recipeName };
            recipes.Add(newRecipe);
        }

        public List<Recipe> GetRecipes()
        {
            return recipes;
        }

        public void UpdateRecipe(Recipe recipeToUpdate, string newName)
        {
            int index = recipes.FindIndex(recipe => recipe == recipeToUpdate);
            if (index != -1)
            {
                recipes[index].Name = newName;
            }
        }

        public void RemoveRecipe(string recipeName)
        {
            Recipe recipeToRemove = null;
            foreach (var recipe in recipes)
            {
                if (recipe.Name == recipeName)
                {
                    recipeToRemove = recipe;
                    break;
                }
            }

            if (recipeToRemove != null)
            {
                recipes.Remove(recipeToRemove);
            }
        }
    }


}


