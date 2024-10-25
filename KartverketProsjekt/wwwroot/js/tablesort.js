function sortTableByColumn(table, column, asc = true) {
    const dirModifier = asc ? 1 : -1;
    const tBody = table.tBodies[0];
    const rows = Array.from(tBody.querySelectorAll("tr"));

    //sort each row
    const sortedRows = rows.sort((a, b) => {

        const aDate = a.querySelector(`td:nth-child(${column + 1})`).textContent.trim();
        const bDate = b.querySelector(`td:nth-child(${column + 1})`).textContent.trim();

        return aDate > bDate ? (1 * dirModifier) : (-1 * dirModifier);
    });

    console.log(sortedRows);

    //remove all existing TRs from the table
    while (tBody.firstChild) {
        tBody.removeChild(tBody.firstChild);
    }

    //re-add the newly sorted rows
    tBody.append(...sortedRows);


    table.querySelectorAll("th").forEach(th => th.classList.remove("th-sort-asc", "th-sort-desc"));
    table.querySelector(`th:nth-child(${column + 1})`).classList.toggle("th-sort-asc", asc);
    table.querySelector(`th:nth-child(${column + 1})`).classList.toggle("th-sort-desc", !asc);
} 



function applyFilter(priority) {
    const table = document.querySelector("table.table-sortable tbody");
    const rows = Array.from(table.querySelectorAll("tr"));

    rows.forEach(row => {
        const priorityCell = row.querySelector("td:nth-child(4)"); //Adjust column index
        const priorityText = priorityCell ? priorityCell.textContent.trim() : "";
        console.log(priorityCell, priorityText);

        if (
            (priority === 'option1' && priorityText === "Høy prioritet") ||
            (priority === 'option2' && priorityText === "Middels prioritet") ||
            (priority === 'option3' && priorityText === "Lav prioritet") ||
            priority === 'all'
        ) {
            row.style.display = ""; //showing row here
        }
        else {
            row.style.display = "none"; //hiding row here
        }

    });

}

function toggleFilterOptions() {
    const filterOptions = document.getElementById("filterOptions"); //getting element by id from html to manipulate its visibility
    filterOptions.classList.toggle("active");
}

sortTableByColumn(document.querySelector("table"), 1, true);

document.querySelectorAll(".table-sortable th").forEach(headerCell => {
    headerCell.addEventListener("click", () => {
        const tableElement = headerCell.parentElement.parentElement.parentElement;
        const headerIndex = Array.prototype.indexOf.call(headerCell.parentElement.children, headerCell);
        const currentIsAscending = headerCell.classList.contains("th-sort-asc");

        sortTableByColumn(tableElement, headerIndex, !currentIsAscending);
    });
});

document.getElementById("filterActive").addEventListener("click", toggleFilterOptions);

