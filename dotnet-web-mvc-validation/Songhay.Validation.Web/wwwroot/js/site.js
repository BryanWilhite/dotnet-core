// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(($) => {
    $(() => {
        if($.validator)
        {
            const highlightInvalidClass = 'invalid-input';

            $.validator.setDefaults({
                errorClass: 'invalid-feedback',
                validClass: 'valid',
                highlight: (element, errorClass, validClass) => {
                    $(element).addClass(highlightInvalidClass).removeClass(validClass);
                    $(element.form)
                        .find(`label[for="${element.id}"]`)
                        .addClass(errorClass);
                },
                unhighlight: (element, errorClass, validClass) => {
                    $(element).removeClass(highlightInvalidClass).addClass(validClass);
                    $(element.form)
                        .find(`label[for="${element.id}"]`)
                        .removeClass(errorClass);
                }
            });
        }

        const todosForm = $('#todo-edit');

        $('.cmd.remove', todosForm).click(e => {
            const removeButton = $(e.target);
            const formGroup = removeButton.closest('.form-group');
            $(formGroup).next().next().remove();
            $(formGroup).next().remove();
            $(formGroup).remove();
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

        $('#cmd-save', todosForm).click(e => {
            const saveButton = $(e.target);
            const div = $(saveButton).closest('div');

            if (todosForm.validate)
            {
                const controls = $('select,input', todosForm);

                for(let control of controls) {
                    const isValid = $(control).valid();

                    if(!isValid) {
                        control.focus();
                        return;
                    }
                }
            }

            $(todosForm).find('.save-status').remove();

            const data = todosForm.serializeArray();
            console.warn({data});

            $.ajax({
                url: '../../Todos/Save/',
                type: 'POST',
                data: data,
                error: (jqXHR, textStatus, errorThrown) => {
                    console.error({jqXHR, textStatus, errorThrown});
                    div.append('<span class="save-status fs-1 text-danger">⚠</span>')
                },
                success: (data, textStatus, jqXHR) => {
                    console.info({data, textStatus, jqXHR});
                    div.append('<span class="save-status fs-1 text-success">✅</span>')
                }
            });
        });
    });
})(jQuery);
