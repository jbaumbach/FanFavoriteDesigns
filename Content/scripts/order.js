// *******************************************
// order.js
// Copyright (C) 2007 Fan Favorite Designs
// *******************************************

function CopyFieldValue(sourceName, destinationName)
{
    var source = document.getElementById(sourceName);
    var dest = document.getElementById(destinationName);
    
    dest.value = source.value;
}

function CopyOrderValueBetweenForms(fieldName)
{
    CopyFieldValue('shippingAddress_' + fieldName, 'billingAddress_' + fieldName);
}

function CopyOrderValues()
{
    CopyOrderValueBetweenForms('txtFName');
    CopyOrderValueBetweenForms('txtLName');
    CopyOrderValueBetweenForms('txtCompanyName');
    CopyOrderValueBetweenForms('txtAddr1');
    CopyOrderValueBetweenForms('txtAddr2');
    CopyOrderValueBetweenForms('txtCity');
    CopyOrderValueBetweenForms('lstState');
    CopyOrderValueBetweenForms('txtZip');
    CopyOrderValueBetweenForms('txtPhone');
}

