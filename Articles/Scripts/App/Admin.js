var example4 = new Taggle('addCategories', { duplicateTagClass: 'bounce' });
var container = example4.getContainer();
var input = example4.getInput();

$.get("/Admin/Tags").done(
    function (tags) {
        $(input).autocomplete({
            source: tags,
            appendTo: container,
            position: { at: "left bottom", of: container },
            select: function (event, data) {
                event.preventDefault();
                //Add the tag if user clicks
                if (event.which === 1) {
                    example4.add(data.item.value);
                }
            }
        });
    }
);