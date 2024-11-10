


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

const createSearchInputElement = () => {
    const el = document.createElement('input');
    el.classList.add('portexe-search-input');
    el.id = 'portexe-search-input';
    el.placeholder = 'Søk...'; // Added placeholder for better UX
    return el;
}


const search = (arr, searchTerm) => {
    if (!searchTerm) return arr;
    return arr.filter(_row => {
        return _row.find(_item => _item.toLowerCase().includes(searchTerm.toLowerCase()));
    });
}

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


const init = () => {
    const searchInput = document.getElementById('searchTable');

    searchInput.addEventListener('keyup', (e) => {
        refreshTable(search(initialTableData, e.target.value));
    });
}

const initialTableData = tableData();


init();

function sortTableByColumn(columnIndex, ascending) {
    if (ascending === undefined) {
        ascending = true;
    }


    const table = document.querySelector(".table-sortable");
    const rows = Array.from(table.querySelectorAll("tbody tr"));

    // Get the data type from the corresponding header
    const type = table.querySelector(`th:nth-child(${columnIndex + 1})`).getAttribute("data-type");

    // Sort each row
    const sortedRows = rows.sort((a, b) => {
        const aText = a.children[columnIndex].innerText;
        const bText = b.children[columnIndex].innerText;

        let aValue;
        let bValue;

        if (type === 'number') {
            aValue = parseFloat(aText);
            bValue = parseFloat(bText);
        } else if (type === 'date') {
            aValue = new Date(aText);
            bValue = new Date(bText);
        } else {
            aValue = aText.toLowerCase();
            bValue = bText.toLowerCase();
        }

        return ascending ? (aValue > bValue ? 1 : -1) : (aValue < bValue ? 1 : -1);
    });

    const tbody = table.querySelector("tbody");
    tbody.innerHTML = ""; // Clear existing rows
    sortedRows.forEach(row => tbody.appendChild(row)); // Append sorted rows
}

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

    rows.forEach(row => {
        const statusCell = row.children[statusColumnIndex];
        const cellStatusValue = statusCell ? statusCell.textContent.trim().toLowerCase() : '';
        const normalizedSelectedStatus = selectedStatus.toLowerCase();

        if (normalizedSelectedStatus === 'all' || cellStatusValue === normalizedSelectedStatus) {
            row.style.display = ''; // Show row
        } else {
            row.style.display = 'none'; // Hide row
        }
    });
}

// Filter the table by Kartlag
function filterTableByKartlag(selectedKartlag) {
    const table = document.getElementById('firstTableId');
    const rows = table.querySelectorAll('tbody tr');
    const kartlagColumnIndex = 4; // Kartlag column is at index 4 (5th column)

    rows.forEach(row => {
        const kartlagCell = row.children[kartlagColumnIndex];
        const cellKartlagValue = kartlagCell ? kartlagCell.textContent.trim().toLowerCase() : '';
        const normalizedSelectedKartlag = selectedKartlag.toLowerCase();

        if (normalizedSelectedKartlag === 'all' || cellKartlagValue === normalizedSelectedKartlag) {
            row.style.display = ''; // Show row
        } else {
            row.style.display = 'none'; // Hide row
        }
    });
}

// Event listener for Status filter
document.getElementById('filterStatusDropdown').addEventListener('change', function () {
    filterTableByStatus(this.value);
});

// Event listener for Kartlag filter
document.getElementById('filterKartlagDropdown').addEventListener('change', function () {
    filterTableByKartlag(this.value);
});

