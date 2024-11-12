
/**
 * Extracts data from a table and returns it as an array of arrays.
 * Each inner array represents a row in the table, with each item corresponding to a cell's content.
 * 
 * This function assumes that the table has an ID of 'firstTableId' and that the data is contained 
 * within the rows of the table (not including headers). It will loop through each row and each 
 * cell within the row to gather the table's data.
 * 
 * @returns {Array} A 2D array representing the table's data, where each inner array is a row,
 *                  and each element in the inner array is a cell's content.
 */

const tableData = () => { 
    const searchData = []; 
    const tableEl = document.getElementById('firstTableId'); 
    Array.from(tableEl.children[1].children).forEach(_bodyRowEl => { 

        searchData.push(Array.from(_bodyRowEl.children).map(_cellEl => { 
            return _cellEl.innerHTML; 
        }));
    });
    return searchData; 
}

/**
 * createSearchInputElement creates an input element for the search functionality.
 * 
 * This function:
 * - Creates a new input element.
 * - Adds a CSS class to style the input.
 * - Sets an ID for the input element for potential reference in the DOM.
 * - Sets a placeholder text for the input field.
 * - Returns the created input element.
 * 
 * @returns {HTMLInputElement} The created search input element.
 */


const createSearchInputElement = () => {    
    const el = document.createElement('input');     //This variable creates an input element and stores it in the variable el
    el.classList.add('portexe-search-input');   
    el.id = 'portexe-search-input';     
    el.placeholder = 'Søk...';  
}


/**
 * Searches through a 2D array of strings and returns rows that contain the search term.
 * The function performs a case-insensitive search across all items in each row.
 * 
 * @param {Array} arr - A 2D array (array of arrays) where each inner array represents a row of data.
 * @param {string} searchTerm - The term to search for in the data. If the term is empty or not provided, all rows are returned.
 * @returns {Array} - A filtered array of rows that contain the search term in at least one of the cells.
 */

const search = (arr, searchTerm) => {       
    if (!searchTerm) return arr;        
    return arr.filter(_row => {        
        return _row.find(_item => _item.toLowerCase().includes(searchTerm.toLowerCase()));  
    });
}


/**
 * Refreshes the contents of a table by updating its rows with new data.
 * 
 * This function clears the current table body and then populates it with 
 * new rows and cells based on the provided `data`. It assumes the table 
 * has an ID of 'firstTableId' and the data is in a 2D array format where 
 * each sub-array represents a row in the table.
 * 
 * @param {Array} data - A 2D array where each sub-array represents a row
 *                       in the table, and each element in the sub-array 
 *                       corresponds to a cell in the row.
 */


const refreshTable = (data) => {
    const tableBody = document.getElementById('firstTableId').children[1];
    tableBody.innerHTML = '';

    data.forEach(_row => {
        const curRow = document.createElement('tr');
        _row.forEach(_dataItem => {
            const curCell = document.createElement('td');
            curCell.innerHTML = _dataItem;
            curRow.appendChild(curCell);
        });
        tableBody.appendChild(curRow);
    });
}


/**
 * Initializes the table search functionality by setting up an event listener 
 * on the search input field. When the user types in the search input, the table 
 * will be dynamically filtered based on the search term.
 * 
 * This function retrieves the search input element, listens for the 'keyup' 
 * event, and filters the table data accordingly by calling the `search` function 
 * and then updating the table with the filtered data using `refreshTable`.
 */

const init = () => {
    const searchInput = document.getElementById('searchTable'); // Get the search input element

    searchInput.addEventListener('keyup', (e) => {
        refreshTable(search(initialTableData, e.target.value));
    });
}

const initialTableData = tableData(); // Extract the initial table data

init();

/**
 * Sorts the rows of a table by a specific column index and order (ascending/descending).
 * The table must have the class `.table-sortable` from html, and each `<th>` should have a `data-type`
 * attribute that indicates the type of data in the column (e.g., 'number', 'date', 'string').
 * 
 * @param {number} columnIndex - The index (0-based) of the column to sort by.
 * @param {boolean} [ascending=true] - A boolean value indicating the sort order. 
 *                                      If `true`, the table will be sorted in ascending order; 
 *                                      if `false`, it will be sorted in descending order.
 */

function sortTableByColumn(columnIndex, ascending) {
    if (ascending === undefined) { 
        ascending = true;
    }


    const table = document.querySelector(".table-sortable");
    const rows = Array.from(table.querySelectorAll("tbody tr"));

    // Get the data type from the corresponding header
    const type = table.querySelector(`th:nth-child(${columnIndex + 1})`).getAttribute("data-type");

    // Sort each row
    const sortedRows = rows.sort((a, b) => { //This loop sorts the rows based on the column index and order
        const aText = a.children[columnIndex].innerText;
        const bText = b.children[columnIndex].innerText;

        let aValue;
        let bValue;

        if (type === 'number') { // Corrected the data type comparison
            aValue = parseFloat(aText);
            bValue = parseFloat(bText);
        } else if (type === 'date') { // Corrected the data type comparison
            aValue = new Date(aText);
            bValue = new Date(bText);
        } else { 
            aValue = aText.toLowerCase();
            bValue = bText.toLowerCase();
        }

        return ascending ? (aValue > bValue ? 1 : -1) : (aValue < bValue ? 1 : -1); // Corrected the comparison operators
    });

    const tbody = table.querySelector("tbody");
    tbody.innerHTML = ""; // Clear existing rows
    sortedRows.forEach(row => tbody.appendChild(row)); // Append sorted rows
}

// In the fuction sortTableByColumn, we are sorting the table rows based on the specified column index and order (ascending or descending).
// The function first identifies the data type (number, date, or text) from the column header,
// then sorts the rows accordingly. After sorting, it clears the current rows in the table body
// and appends the sorted rows to update the displayed order.


    
let currentSortColumn = null;
let currentSortAscending = true;

// Add click event listeners to headers
document.querySelectorAll(".table-sortable th").forEach((header, index) => { // Corrected arrow function
    header.addEventListener("click", () => { // Corrected the event listener method
        if (currentSortColumn === index) {
            currentSortAscending = !currentSortAscending; // Toggle sort direction
        } else {
            currentSortAscending = true; // Default to ascending
            currentSortColumn = index; // Set the new sort column
        }

        // Clear previous sort indicators
        document.querySelectorAll(".table-sortable th").forEach(th => {
            th.classList.remove("sorted-asc", "sorted-desc");
        });

        // Set sort indicators for the current column
        header.classList.toggle("sorted-asc", currentSortAscending);
        header.classList.toggle("sorted-desc", !currentSortAscending);

        // Call the sorting function
        sortTableByColumn(currentSortColumn, currentSortAscending);
    });
});


// Filter the table by status
function filterTableByStatus(selectedStatus) {
    const table = document.getElementById('firstTableId');
    const rows = table.querySelectorAll('tbody tr');
    const statusColumnIndex = 6; // Status column is at index 6 (7th column)

    rows.forEach(row => { //this loop goes through each row in the table 'tbody' of the table
        const statusCell = row.children[statusColumnIndex]; //this variable stores the status cell of the row
        const cellStatusValue = statusCell ? statusCell.textContent.trim().toLowerCase() : ''; //this variable stores the status value of the cell
        const normalizedSelectedStatus = selectedStatus.toLowerCase();

        if (normalizedSelectedStatus === 'all' || cellStatusValue === normalizedSelectedStatus) { //if the selected status is 'all' or the cell status value matches the selected status
            row.style.display = ''; // Show row
        } else {
            row.style.display = 'none'; // Hide row
        }
    });
}

// Filter the table by Kartlag
function filterTableByKartlag(selectedKartlag) { //This functions takes a parameter that is the selected kartlag
    const table = document.getElementById('firstTableId');
    const rows = table.querySelectorAll('tbody tr');
    const kartlagColumnIndex = 4; // Kartlag column is at index 4 (5th column)

    rows.forEach(row => { //this loop goes through each row in the table 'tbody' of the table
        const kartlagCell = row.children[kartlagColumnIndex]; //this variable stores the kartlag cell of the row we are currently on
        const cellKartlagValue = kartlagCell ? kartlagCell.textContent.trim().toLowerCase() : '';
        const normalizedSelectedKartlag = selectedKartlag.toLowerCase(); //this variable stores the selected kartlag value

        if (normalizedSelectedKartlag === 'all' || cellKartlagValue === normalizedSelectedKartlag) { //if the selected kartlag is 'all' or the cell kartlag value matches the selected kartlag
            row.style.display = ''; // Show row
        } else {
            row.style.display = 'none'; // Hide row
        }
    });
}

// Filters table rows based on the selected "Kartlag" value in the fifth column.
// Shows rows that match the selected value or all rows if "all" is selected; hides others.


// Combined filtering function
function filterTable() {
    const table = document.getElementById('firstTableId');
    const rows = table.querySelectorAll('tbody tr');
    const statusColumnIndex = 6; // Status column is at index 6
    const kartlagColumnIndex = 4; // Kartlag column is at index 4

    const selectedStatus = document.getElementById('filterStatusDropdown').value.toLowerCase();
    const selectedKartlag = document.getElementById('filterKartlagDropdown').value.toLowerCase();

    rows.forEach(row => { // this loop goes through each row in the table 'tbody' of the table
        const statusCell = row.children[statusColumnIndex]; 
        const kartlagCell = row.children[kartlagColumnIndex];

        const cellStatusValue = statusCell ? statusCell.textContent.trim().toLowerCase() : '';
        const cellKartlagValue = kartlagCell ? kartlagCell.textContent.trim().toLowerCase() : '';

        // Check if both status and kartlag match the selected values
        const statusMatch = (selectedStatus === 'all' || cellStatusValue === selectedStatus);
        const kartlagMatch = (selectedKartlag === 'all' || cellKartlagValue === selectedKartlag);

        // Show or hide row based on the combined status and kartlag filter
        if (statusMatch && kartlagMatch) {
            row.style.display = ''; // Show row if both filters match
        } else {
            row.style.display = 'none'; // Hide row if filters don't match
        }
    });
}

// The filterTable filters the table rows based on selected values from two dropdowns: "Status" and "Kartlag".
// Only rows matching both selected filters (or 'all' values) are displayed, while others are hidden.


// Event listener for Status filter 
document.getElementById('filterStatusDropdown').addEventListener('change', filterTable);

// Event listener for Kartlag filter
document.getElementById('filterKartlagDropdown').addEventListener('change', filterTable);
