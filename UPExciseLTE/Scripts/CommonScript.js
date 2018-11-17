$('#ddlBrand').change(function () {
    var BrandId = $('#ddlBrand').val();
    if (BrandId != "-1") {
        BrandChange(BrandId);
    }
});

function BrandChange(BrandId) {
    $.ajax({
        url: '/MasterForms/GetBrandDetailsForDDl',
        data: { 'BrandId': BrandId },
        datatype: "json",
        type: "GET",
        success: function (data) {
            var res = data.split(",");
            //document.getElementById("txtLiquorType").value = res[0];
            //document.getElementById("txtLicenseType").value = res[1];
            document.getElementById("txtLicenseNo").value = res[2];
            //document.getElementById("txtBottleInCase").value = res[3];
            //document.getElementById("txtQuantityInBottle").value = res[4];
            //document.getElementById("txtStrength").value = res[5];
            //var element = document.getElementById("ddlState");
            //element.value = res[6];
            //$('#ddlState').attr('disabled', true);
        },
        error: function (data) {
            alert("Error in ModelShop Loading");
        }
    });
}

 

   

function ValidateExtension(eve) {
    //var allowedFiles = [".csv", ".xls", ".xlsx"];
    var allowedFiles = [".csv"];
    var fileUpload = document.getElementById($(eve).prev().prev().attr('id'));
    var lblError = document.getElementById("lblError");
    var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+" + allowedFiles.join('|') + "$");
    if (!regex.test(fileUpload.value.toLowerCase())) {
        lblError.innerHTML = "Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.";
        $("#danger-alert").show();
        $("#danger-alert").fadeTo(3000, 500).slideUp(500, function () {
            $("#danger-alert").slideUp(500);
        });
        return false;
    }
    lblError.innerHTML = "";
    //alert("File Uploaded Successfully.");
    return true;
}
var specialKeys = new Array();
specialKeys.push(47); // for Forward Slash(/)
specialKeys.push(8); //Backspace
specialKeys.push(9); // Tab
specialKeys.push(37); // for left Arrow
//  specialKeys.push(39); // for Right Arrow
function IsNumeric(e) {
    var keyCode = e.which ? e.which : e.keyCode
    var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1); // For Decimal To All Number
    if (ret == false) {
        //alert("Only numaric value allowed.");
    }
    return ret;
}
function isfloatNumber(txt, evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 46) {
        //Check if the text already contains the . character
        if (txt.value.indexOf('.') === -1) {
            return true;
        } else {
            return false;
        }
    } else {
        if (charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;
    }
    return true;
}

