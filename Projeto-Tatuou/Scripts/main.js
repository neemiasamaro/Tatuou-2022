$(document).ready(function () {
    $('#cep').mask("00000-000")
    $('#cpf').mask("000.000.000-00")
    $('#cnpj').mask("00.000.000/0000-00")
    $('#whatsapp').mask("(00) 0000-00009")
    $('#whatsapp').blur(function (event) {
        if ($(this).val().length == 15) {
            $('#whatsapp').mask("(00) 00000-0009")
        }
        else {
            $('#whatsapp').mask("(00) 0000-00009")
        }
        if ($(this).val().length == 14) {
            $('#whatsapp').mask("(00) 0000-0000")
        }
    })

    $('#show').click(function () {
        if ($(this).is(":checked")) {
            document.getElementById('password').type = 'text';
            document.getElementById('cpassword').type = 'text';
        }
        else {
            document.getElementById('password').type = 'password';
            document.getElementById('cpassword').type = 'password';
        }
    })
});

const cep = document.querySelector("#cep")

const showData = (result) => {
    for (const campo in result) {
        if (document.querySelector("#" + campo)) {
            $("input[id=logradouro]").val(result.logradouro)
            $("input[id=bairro]").val(result.bairro)
            $("input[id=localidade]").val(result.localidade)
            $("input[id=uf]").val(result.uf)
        }
    }
}

cep.addEventListener('blur', (e) => {
    let search = cep.value.replace("-", "")
    const options = {
        method: 'GET',
        mode: 'cors',
        cache: 'default'
    }

    fetch(`https://viacep.com.br/ws/${search}/json/`, options)
        .then(response => {
            response.json().then(data => showData(data))
        }).catch(e => console.log('Deu Erro: ' + e, message))
})

$(document).ready(function () {
    'use strict'

    var photo = document.getElementById('imgPhoto');
    var file = document.getElementById('img-input');

    photo.addEventListener('click', () => {
        file.click();
    })

    file.addEventListener('change', () => {

        if (file.files.length <= 0) {
            return;
        }

        var reader = new FileReader();

        reader.onload = () => {
            photo.src = reader.result;
        }

        reader.readAsDataURL(file.files[0]);
    });
})