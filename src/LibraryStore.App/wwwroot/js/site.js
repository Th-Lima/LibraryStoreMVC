function AjaxModal() {

    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                keyboard: true
                            },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });
    });
}

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#TargetAddress').load(result.url); // Carrega o resultado HTML para a div demarcada
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });

        AjaxModal();
        return false;
    });
}

function SearchCep() {
    $(document).ready(function () {

        function clearFormCep() {
            // Limpa valores do formulário de cep.
            $("#Address_AddressPlace").val("");
            $("#Address_Neighborhood").val("");
            $("#Address_City").val("");
            $("#Address_State").val("");
        }

        //Quando o campo cep perde o foco.
        $("#Address_ZipCode").blur(function () {

            //Nova variável "cep" somente com dígitos.
            var cep = $(this).val().replace(/\D/g, '');

            //Verifica se campo cep possui valor informado.
            if (cep != "") {

                //Expressão regular para validar o CEP.
                var validateCep = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (validateCep.test(cep)) {

                    //Preenche os campos com "..." enquanto consulta webservice.
                    $("#Address_AddressPlace").val("...");
                    $("#Address_Neighborhood").val("...");
                    $("#Address_City").val("...");
                    $("#Address_State").val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (data) {

                            if (!("erro" in data)) {
                                //Atualiza os campos com os valores da consulta.
                                $("#Address_AddressPlace").val(data.logradouro);
                                $("#Address_Neighborhood").val(data.bairro);
                                $("#Address_City").val(data.localidade);
                                $("#Address_State").val(data.uf);
                            } //end if.
                            else {
                                //CEP pesquisado não foi encontrado.
                                clearFormCep();
                                alert("CEP não encontrado.");
                            }
                        });
                } //end if.
                else {
                    //cep é inválido.
                    clearFormCep();
                    alert("Formato de CEP inválido.");
                }
            } //end if.
            else {
                //cep sem valor, limpa formulário.
                clearFormCep();
            }
        });
    });
}

$(document).ready(function () {
    $("#msg_box").fadeOut(2900);
});