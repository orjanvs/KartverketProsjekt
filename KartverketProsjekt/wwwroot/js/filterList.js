
//This js is responsible for the search and filter functionality for the table in the Listform page

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

// <> Summary <>
//The const tableData is a function that returns an array of data from the table element.
//The function creates an empty array called searchData.
//Then we create a variable called tableEl that stores the table element.
//We then create an array from the table body rows using the Array.from method.
//We then loop through the table body rows using the forEach method.
//We then push the table data to the searchData array using the push method.
//We then return the innerHTML of the cell element using the return statement.
//Finally, we return the searchData array after the loop is done.


const createSearchInputElement = () => {    
    const el = document.createElement('input');     //This variable creates an input element and stores it in the variable el
    el.classList.add('portexe-search-input');   
    el.id = 'portexe-search-input';     
    el.placeholder = 'Søk...';  
}

// The createSearchInputElement function =>
// Creates a configured search input element with a class, ID, and placeholder, ready to be added to the UI.


const search = (arr, searchTerm) => {       
    if (!searchTerm) return arr;        
    return arr.filter(_row => {        
        return _row.find(_item => _item.toLowerCase().includes(searchTerm.toLowerCase()));  
    });
}

// The const - search, filters an array of rows based on a search term. (arr.filter)
// Returns the original array if the search term is empty
// or filters rows containing items that include the search term (case-insensitive).


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

//<> Summary <>
// The refresh table is a function above takes in data as a parameter.
//The function creates a variable called tableBody that stores the table body element.
//The innerHTML of the table body is set to an empty string.The data is then looped through using the forEach method.
//A new row is created for each item in the data array.The data item is then looped through and a new cell is created for each item in the data array.
//The innerHTML of the cell is set to the data item.The cell is then appended to the row.The row is then appended to the table body.
//<> Summary <>


const init = () => {
    const searchInput = document.getElementById('searchTable'); // Get the search input element

    searchInput.addEventListener('keyup', (e) => {
        refreshTable(search(initialTableData, e.target.value));
    });
}

const initialTableData = tableData();

init();

// The const init above initializes a search input listener for filtering table data in real-time.
// This is done by getting the searchtable element by id from the html file.
// We then add an event listener to the search input element that listens for the keyup event (when the keyboard is not pressed).
// After that, we call the refreshTable function which takes two parameter. The original table data and the search input value.
// The refreshTable function then refreshes the table data based on the search input value.


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
