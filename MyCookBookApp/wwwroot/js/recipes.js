document.addEventListener('DOMContentLoaded', function () {
    // Pre-added recipes
    const preAddedRecipes = [
        {
            name: "Boba",
            tagLine: "Sweet and Refreshing",
            summary: "A classic Taiwanese drink with chewy tapioca pearls.",
            ingredients: ["Tapioca pearls", "Milk", "Brown sugar", "Ice"],
            instructions: [
                "Cook tapioca pearls according to package instructions.",
                "Mix brown sugar with water to create a syrup.",
                "Add cooked pearls to a glass, pour in milk, and add ice.",
                "Stir and enjoy!"
            ],
            categories: ["Drink", "Dessert"]
        },
        {
            name: "Chicken Curry",
            tagLine: "Spicy and Flavorful",
            summary: "A hearty dish with tender chicken and aromatic spices.",
            ingredients: ["Chicken", "Curry powder", "Coconut milk", "Onions", "Garlic"],
            instructions: [
                "Sauté onions and garlic in a pan.",
                "Add chicken and cook until browned.",
                "Stir in curry powder and coconut milk.",
                "Simmer until the chicken is cooked through.",
                "Serve with rice or bread."
            ],
            categories: ["Dinner", "Spicy"]
        },
        {
            name: "Ranchero Pizza",
            tagLine: "Savory and Cheesy",
            summary: "A Mexican-inspired pizza with bold flavors.",
            ingredients: ["Pizza dough", "Tomato sauce", "Cheese", "Chorizo", "Bell peppers"],
            instructions: [
                "Preheat the oven to 425°F (220°C).",
                "Roll out the pizza dough and spread tomato sauce.",
                "Top with cheese, chorizo, and bell peppers.",
                "Bake for 12-15 minutes or until the crust is golden.",
                "Slice and serve!"
            ],
            categories: ["Lunch", "Mexican"]
        }
    ];

    // Display pre-added recipes
    displayRecipes(preAddedRecipes);

    // Open the Add Recipe Modal
    document.getElementById('addRecipeBtn')?.addEventListener('click', function () {
        document.getElementById('addRecipeForm').reset(); // Reset the form
        $('#addRecipeModal').modal('show'); // Show the modal
    });

    // Save Recipe
    document.getElementById('saveRecipe')?.addEventListener('click', function () {
        const recipe = {
            name: document.getElementById('recipeName').value.trim(),
            tagLine: document.getElementById('tagLine').value.trim(),
            summary: document.getElementById('summary').value.trim(),
            ingredients: document.getElementById('ingredients').value.split(',').map(i => i.trim()),
            instructions: document.getElementById('instructions').value.split('\n').map(i => i.trim()),
            categories: Array.from(document.getElementById('categories').selectedOptions).map(option => option.value)
        };

        if (!recipe.name || !recipe.ingredients.length || !recipe.instructions.length) {
            alert('Please fill out all required fields.');
            return;
        }

        // Add the new recipe to the list
        preAddedRecipes.push(recipe);

        // Display the updated list of recipes
        displayRecipes(preAddedRecipes);

        // Hide the modal
        $('#addRecipeModal').modal('hide');
    });

    // Search Recipes
    document.getElementById('searchButton')?.addEventListener('click', function () {
        const query = document.getElementById('searchInput').value.trim().toLowerCase();
        if (!query) {
            // If the search query is empty, display all recipes
            displayRecipes(preAddedRecipes);
            return;
        }

        // Filter recipes based on the search query
        const filteredRecipes = preAddedRecipes.filter(recipe =>
            recipe.name.toLowerCase().includes(query) ||
            recipe.tagLine.toLowerCase().includes(query) ||
            recipe.summary.toLowerCase().includes(query) ||
            recipe.ingredients.some(ingredient => ingredient.toLowerCase().includes(query)) ||
            recipe.instructions.some(instruction => instruction.toLowerCase().includes(query)) ||
            recipe.categories.some(category => category.toLowerCase().includes(query))
        );

        // Display the filtered recipes
        displayRecipes(filteredRecipes);
    });

    // Display Recipes
    function displayRecipes(recipes) {
        const recipeCards = document.getElementById('recipeCards');
        recipeCards.innerHTML = ''; // Clear existing cards

        recipes.forEach(recipe => {
            const card = `
                <div class="col-md-6 col-lg-4">
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title">${recipe.name}</h5>
                            <p class="text-muted">${recipe.tagLine}</p>
                            <h6 class="text-muted">Summary:</h6>
                            <p class="card-text">${recipe.summary}</p>
                            <h6 class="text-muted">Ingredients:</h6>
                            <ul class="list-group list-group-flush">
                                ${recipe.ingredients.map(ingredient => `<li class="list-group-item">${ingredient}</li>`).join('')}
                            </ul>
                            <h6 class="mt-3">Instructions:</h6>
                            <ol>
                                ${recipe.instructions.map(step => `<li>${step}</li>`).join('')}
                            </ol>
                            <h6 class="mt-3">Categories:</h6>
                            <p class="card-text">${recipe.categories.join(', ')}</p>
                        </div>
                    </div>
                </div>
            `;
            recipeCards.innerHTML += card;
        });
    }
});