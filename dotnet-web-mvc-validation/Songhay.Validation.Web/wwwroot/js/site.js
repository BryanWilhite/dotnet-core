// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(($) => {
    $(() => {
        const todosForm = $('#todo-edit');

        $('.cmd.remove', todosForm).click(e => {
            console.info('remove', e);
        });

        $('#cmd-add', todosForm).click(_ => {
            const id = $('#Id', todosForm).val();
            const name = $('#Name', todosForm).val();

            $.ajax({
                url: '../../Todos/AddRow/',
                type: 'POST',
                data: { id, name },
                error: (jqXHR, textStatus, errorThrown) => {
                    console.error({jqXHR, textStatus, errorThrown});
                },
                success: (data, textStatus, jqXHR) => {
                    console.info({data, textStatus, jqXHR});

                    $('#todo-items-group').append(data);

                }
            });
        });

        $('#cmd-save', todosForm).click(_ => {
            $.ajax({
                url: '../../Todos/Save/',
                type: 'POST',
                data: todosForm.serializeArray(),
                error: (jqXHR, textStatus, errorThrown) => {
                    console.error({jqXHR, textStatus, errorThrown});
                },
                success: (data, textStatus, jqXHR) => {
                    console.info({data, textStatus, jqXHR});
                }
            });
        });
    });
})(jQuery);
