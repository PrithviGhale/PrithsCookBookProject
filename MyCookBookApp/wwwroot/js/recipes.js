document.addEventListener('DOMContentLoaded', function () {
    const apiUrl = "https://localhost:7238/api/recipe";

    function loadRecipes() {
        fetch(apiUrl)
            .then(res => res.json())
            .then(recipes => displayRecipes(recipes))
            .catch(err => {
                console.error("Error loading recipes:", err);
                alert("Failed to load recipes.");
            });
    }

    function displayRecipes(recipes) {
        const recipeCards = document.getElementById('recipeCards');
        recipeCards.innerHTML = '';

        recipes.forEach(recipe => {
            const card = `
                <div class="col-md-6 col-lg-4">
                    <div class="card mb-4 shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title">${recipe.name}</h5>
                            <p class="text-muted">${recipe.tagLine || ''}</p>
                            <p>${recipe.summary || ''}</p>
                            <h6>Ingredients:</h6>
                            <ul>${(recipe.ingredients || []).map(i => `<li>${i}</li>`).join('')}</ul>
                            <h6>Instructions:</h6>
                            <ol>${(recipe.instructions || []).map(i => `<li>${i}</li>`).join('')}</ol>
                            <h6>Categories:</h6>
                            <p>${(recipe.categories || []).join(', ')}</p>
                            <div class="d-flex justify-content-end">
                                <button class="btn btn-warning btn-sm me-2" onclick="editRecipe('${recipe.recipeId}')">Edit</button>
                                <button class="btn btn-danger btn-sm" onclick="deleteRecipe('${recipe.recipeId}')">Delete</button>
                            </div>
                        </div>
                    </div>
                </div>
            `;
            recipeCards.innerHTML += card;
        });
    }

    document.getElementById('saveRecipe')?.addEventListener('click', function () {
        const recipe = {
            recipeId: crypto.randomUUID(),
            name: document.getElementById('recipeName').value.trim(),
            tagLine: document.getElementById('tagLine').value.trim(),
            summary: document.getElementById('summary').value.trim(),
            ingredients: document.getElementById('ingredients').value.split(',').map(i => i.trim()).filter(i => i),
            instructions: document.getElementById('instructions').value.split('\n').map(i => i.trim()).filter(i => i),
            categories: Array.from(document.getElementById('categories').selectedOptions).map(opt => opt.value),
            media: []
        };

        if (!recipe.name || !recipe.ingredients.length || !recipe.instructions.length) {
            alert('Please fill out all required fields.');
            return;
        }

        fetch(apiUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(recipe)
        })
            .then(res => {
                if (!res.ok) throw new Error("Failed to save recipe");
                return res.json();
            })
            .then(() => {
                $('#addRecipeModal').modal('hide');
                loadRecipes();
            })
            .catch(err => {
                console.error("Error adding recipe:", err);
                alert("Error adding recipe.");
            });
    });

    window.editRecipe = function (id) {
        fetch(`${apiUrl}/${id}`)
            .then(res => {
                if (!res.ok) throw new Error("Recipe not found");
                return res.json();
            })
            .then(recipe => {
                document.getElementById('editRecipeId').value = recipe.recipeId || '';
                document.getElementById('editRecipeName').value = recipe.name || '';
                document.getElementById('editTagLine').value = recipe.tagLine || '';
                document.getElementById('editSummary').value = recipe.summary || '';
                document.getElementById('editIngredients').value = (recipe.ingredients || []).join(', ');
                document.getElementById('editInstructions').value = (recipe.instructions || []).join('\n');
                document.getElementById('editCategories').value = (recipe.categories || []).join(', ');

                $('#editRecipeModal').modal('show');
            })
            .catch(err => {
                console.error("Edit failed:", err);
                alert("Failed to load recipe for editing.");
            });
    };

    document.getElementById('updateRecipe')?.addEventListener('click', function () {
        const recipeId = document.getElementById('editRecipeId')?.value?.trim();

        if (!recipeId) {
            alert("Missing Recipe ID. Cannot update.");
            return;
        }

        const updated = {
            recipeId: recipeId,
            name: document.getElementById('editRecipeName').value.trim(),
            tagLine: document.getElementById('editTagLine').value.trim(),
            summary: document.getElementById('editSummary').value.trim(),
            ingredients: document.getElementById('editIngredients').value.split(',').map(i => i.trim()).filter(i => i),
            instructions: document.getElementById('editInstructions').value.split('\n').map(i => i.trim()).filter(i => i),
            categories: document.getElementById('editCategories').value.split(',').map(i => i.trim()).filter(i => i),
            media: []
        };

        fetch(`${apiUrl}/${recipeId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(updated)
        })
            .then(res => {
                if (!res.ok) throw new Error("Failed to update recipe");
                $('#editRecipeModal').modal('hide');
                loadRecipes();
            })
            .catch(err => {
                console.error("Update failed:", err);
                alert("Error updating recipe.");
            });
    });

    window.deleteRecipe = function (id) {
        if (!id) {
            alert("Missing Recipe ID. Cannot delete.");
            return;
        }

        if (!confirm("Are you sure you want to delete this recipe?")) return;

        fetch(`${apiUrl}/${id}`, {
            method: "DELETE"
        })
            .then(res => {
                if (!res.ok) throw new Error("Failed to delete recipe");
                loadRecipes();
            })
            .catch(err => {
                console.error("Delete failed:", err);
                alert("Error deleting recipe.");
            });
    };

    document.getElementById('searchButton')?.addEventListener('click', function () {
        const keyword = document.getElementById('searchInput').value.trim();

        if (!keyword) {
            loadRecipes();
            return;
        }

        fetch(`${apiUrl}/search`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ keyword: keyword })
        })
            .then(res => res.json())
            .then(data => displayRecipes(data))
            .catch(err => {
                console.error("Search failed:", err);
                alert("Search failed.");
            });
    });

    loadRecipes();
});