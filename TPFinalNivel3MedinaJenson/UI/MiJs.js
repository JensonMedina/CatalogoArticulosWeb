
function validar(ddlSelector, txtSelector) {
    //const ddlCriterio = document.getElementById("ddlCriterio");
    //const txtFiltro = document.getElementById("txtFiltro");
    const ddlCriterio = document.getElementById("ddlSelector");
    const txtFiltro = document.getElementById("txtSelector");

    console.log(ddlCriterio, txtFiltro)

    if (ddlCriterio.value == "") {
        ddlCriterio.classList.add("is-invalid");
        ddlCriterio.classList.remove("is-valid");
        return false;
    }
    if (txtFiltro.value == "") {
        txtFiltro.classList.add("is-invalid");
        txtFiltro.classList.remove("is-valid");
        return false;
    }
    
    txtFiltro.classList.remove("is-invalid");
    txtFiltro.classList.add("is-valid");
    ddlCriterio.classList.remove("is-invalid");
    ddlCriterio.classList.add("is-valid");
    return true;
}