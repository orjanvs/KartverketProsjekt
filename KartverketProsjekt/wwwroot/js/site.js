function FilterOptions() {
    const activFilter = document.getElementById("filterOptions");
    activFilter.classList.toggle("active");
}

function applyFilter(option) {
    const listItems = document.querySelectorAll(".list-item");

    // Reset display for all items
    listItems.forEach(element => {
        element.style.display = "block"; // Show all items initially
    });

    // Apply filter based on the option selected
    if (option === 'option1') { // Filter by date
        const today = new Date().toISOString().split('T')[0]; // Get today's date
        listItems.forEach(element => {
            if (element.getAttribute('data-dato') !== today) {
                element.style.display = "none"; // Hide items not matching the date
            }
        });
    } else if (option === 'option2') { // Filter by profession
        listItems.forEach(element => {
            if (element.getAttribute('data-profession') !== 'SomeProfession') {
                element.style.display = "none"; // Hide non-matching professions
            }
        });
    } else if (option === 'option5') { // Filter by high priority
        listItems.forEach(element => {
            if (element.getAttribute('data-prioritet') !== 'høy') {
                element.style.display = "none"; // Hide items without high priority
            }
        });
    }
}
