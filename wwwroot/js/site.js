const uri = 'api/v1/person';
let persons = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

async function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addDNameTextbox = document.getElementById('add-displayName');
    const addSkillsTextbox = document.getElementById('add-skills');

    const item = {
        id: 0,
        displayName: addDNameTextbox.value.trim(),
        name: addNameTextbox.value.trim(),
        skills: []
    };

    const skillLevels = addSkillsTextbox.value.split(',');

    skillLevels.forEach(skil => {
        const skillLevelArray = skil.split('/');
        const skill = {
            personid: 0,
            skillName: skillLevelArray[0].trim(),
            level: Number.parseInt(skillLevelArray[1], 10)
        }
        item.skills.push(skill);
    })

    let response = await fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(item)
    });
    document.getElementById('errors').style.display = 'none';
    document.getElementById('errors').innerHTML = '';
    if (response.ok) {
        getItems();
        addNameTextbox.value = '';
        addDNameTextbox.value = '';
        addSkillsTextbox.value = '';
    }
    else {
        const errorData = await response.json();
        console.log("errors", errorData);
        if (errorData) {
            if (errorData.errors) {
                for (var key in errorData.errors) {
                    addError(errorData.errors[`${key}`]);
                }
            }
        }
        document.getElementById("errors").style.display = 'block';
    }
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = persons.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-displayName').value = item.displayName;
    let textNode3 = "";
    for (let i = 0; i < item.skills.length; i++) {
        textNode3 += item.skills[i].skillName + "/" + item.skills[i].level + ",";
    }
    document.getElementById('edit-skills').value = textNode3.substring(0, textNode3.length - 1);
    document.getElementById('editForm').style.display = 'block';
}

function displayGetForm(data) {

    const tBody = document.getElementById('person');
    tBody.innerHTML = '';
    let tr = tBody.insertRow();

    let td1 = tr.insertCell(0);
    let textNode1 = document.createTextNode(data.displayName);
    td1.appendChild(textNode1);

    let td2 = tr.insertCell(1);
    let textNode2 = document.createTextNode(data.name);
    td2.appendChild(textNode2);

    document.getElementById('getForm').style.display = 'block';
}

function getItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'GET'
    })
        .then(response => response.json())
        .then(data => displayGetForm(data))
        .catch(error => console.error('Unable to update item.', error));

    closeGet();

    return false;
}

async function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        displayName: document.getElementById('edit-displayName').value.trim(),
        name: document.getElementById('edit-name').value.trim(),
        skills: []
    };

    const editSkillsTextbox = document.getElementById('edit-skills');
    const skillLevels = editSkillsTextbox.value.split(',');

    skillLevels.forEach(skil => {
        const skillLevelArray = skil.split('/');
        const skill = {
            personid: parseInt(itemId, 10),
            skillName: skillLevelArray[0].trim(),
            level: Number.parseInt(skillLevelArray[1], 10)
        }
        item.skills.push(skill);
    });

    let response = await fetch(uri, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(item)
    });
    document.getElementById('errors').style.display = 'none';
    document.getElementById('errors').innerHTML = '';
    if (response.ok) {
        getItems();
        closeInput();
    }
    else {
        const errorData = await response.json();
        console.log("errors", errorData);
        if (errorData) {
            if (errorData.errors) {
                for (var key in errorData.errors) {
                    addError(errorData.errors[`${key}`]);
                }
            }
        }
        document.getElementById("errors").style.display = 'block';
    }
    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
    document.getElementById('errors').style.display = 'none';
    document.getElementById('errors').innerHTML = '';
}

function closeGet() {
    document.getElementById('getForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'person' : 'persons';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('persons');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let getButton = button.cloneNode(false);
        getButton.innerText = 'Get';
        getButton.setAttribute('onclick', `getItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode1 = document.createTextNode(item.displayName);
        td1.appendChild(textNode1);

        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(item.name);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        let textNode3 = "";
        for (let i = 0; i < item.skills.length; i++) {
            textNode3 += item.skills[i].skillName + "(" + item.skills[i].level + "), ";
        }
        textNode3 = document.createTextNode(textNode3.substring(0, textNode3.length - 2));
        td3.appendChild(textNode3);

        let td4 = tr.insertCell(3);
        td4.appendChild(getButton);

        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(5);
        td6.appendChild(deleteButton);
    });

    persons = data;
}

function addError(error) {
    const p = document.createElement("p");
    p.append(error);
    document.getElementById("errors").append(p);
}
