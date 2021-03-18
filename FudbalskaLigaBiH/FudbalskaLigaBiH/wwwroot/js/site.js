// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function PotvrdaBrisanja(ID, kliknuto) {
    var spanBrisanje = 'spanBrisanje_' + ID;
    var PotvrdaBrisanjaSpan = 'PotvrdaBrisanjaSpan_' + ID;

    if (kliknuto) {
        $('#' + spanBrisanje).hide();
        $('#' + PotvrdaBrisanjaSpan).show();
    }
    else {
        $('#' + spanBrisanje).show();
        $('#' + PotvrdaBrisanjaSpan).hide();
    }

}