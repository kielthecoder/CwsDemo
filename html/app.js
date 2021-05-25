const nameInput = document.getElementById('name');
const locationInput = document.getElementById('location');
const helpNumberInput = document.getElementById('helpNumber');

const inputs = document.getElementById('inputs');
const outputs = document.getElementById('outputs');

const saveBtn = document.getElementById('save-changes');
saveBtn.addEventListener('click', (e) => {
    putSettings();
});

async function putSettings () {
    newSettings = {
        name: nameInput.value,
        location: locationInput.value,
        helpNumber: helpNumberInput.value,
        inputs: [],
        outputs: []
    }

    Array.from(inputs.getElementsByTagName('INPUT')).forEach((item) => {
        newSettings.inputs.push(item.value);
    });

    Array.from(outputs.getElementsByTagName('INPUT')).forEach((item) => {
        newSettings.outputs.push(item.value);
    });

    let result = await fetch('/cws/api/room', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newSettings)
    });
    let data = await result.json();

    if (data.status === 'OK') {
        Array.from(document.getElementsByTagName('INPUT')).forEach((item) => {
            item.style.borderColor = 'black';
        });

        saveBtn.style.visibility = 'hidden';
    }
}

async function getSettings () {
    let result = await fetch('/cws/api/room');
    let data = await result.json();

    nameInput.value = data.name;
    locationInput.value = data.location;
    helpNumberInput.value = data.helpNumber;

    let i = 1;
    
    data.inputs.forEach((item) => {
        let div = document.createElement('div');
        
        let label = document.createElement('label');
        label.setAttribute('for', `input-${i}`);
        label.innerText = `Input ${i}: `;
        div.appendChild(label);
        
        div.appendChild(document.createTextNode(' '));

        let textField = document.createElement('input');
        textField.setAttribute('type', 'text');
        textField.setAttribute('id', `input-${i}`);
        textField.setAttribute('value', item);
        div.appendChild(textField);

        inputs.appendChild(div);
        i += 1;
    });

    i = 1;

    data.outputs.forEach((item) => {
        let div = document.createElement('div');
        
        let label = document.createElement('label');
        label.setAttribute('for', `output-${i}`);
        label.innerText = `Output ${i}: `;
        div.appendChild(label);

        div.appendChild(document.createTextNode(' '));

        let textField = document.createElement('input');
        textField.setAttribute('type', 'text');
        textField.setAttribute('id', `output-${i}`);
        textField.setAttribute('value', item);
        div.appendChild(textField);

        outputs.appendChild(div);
        i += 1;
    });

    Array.from(document.getElementsByTagName('INPUT')).forEach((item) => {
        item.addEventListener('change', () => {
            item.style.borderColor = 'red';
            saveBtn.style.visibility = 'visible';
        });
    });
}

setTimeout(getSettings, 100);