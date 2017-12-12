///<summary>
/// Written by Claus and Jonas
///</summary>

$(document).ready(
    function () {

        $('.placeholder').slideUp();
        $('.slide').slideUp();
        $(".line").click(
            function () {

                $(this).children(".slide").slideToggle();
                $(this).nextAll().children(".slide").slideToggle();

                $(this).nextAll().children(".placeholder").slideToggle();
                $(this).nextAll().children(".hideFiscal").slideToggle();
            }
        );
    }
);