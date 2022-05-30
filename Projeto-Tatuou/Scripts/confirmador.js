function confirma(id) {
    let resposta = confirm("Tem certeza que deseja excluir este item?")
    if (resposta == true) {
        window.location.href = "Estilos/Delete/" + id;
    }
}