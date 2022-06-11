// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(($) => {
    $(() => {

        const formatNewFieldGroup = fieldset => {
            $('.form-group', fieldset).each((_, div) => {
                const label = $('label', div);
                const input = $('input', div);
                const id_prefix = 'Items_0__';
                const name_prefix = 'Items[0].';
                const input_id = `${id_prefix}${input.attr('id')}`;
                const input_name = `${name_prefix}${input.attr('name')}`;

                label.attr('for', input_id);
                input.attr('id', input_id);
                input.attr('name', input_name);

                if(input.attr('name').endsWith('.Name')) {
                    input.val('');
                }
            });

            $('legend', fieldset).val('New TODO Item');
        };

        const renumberFormGroup = (form, fieldset, i) => {
            $('.form-group', fieldset).each((_, div) => {
                const label = $('label', div);
                const input = $('input', div);
                const original_input_name = input.attr('name');
                const regex = /[\[_](\d+)[_\]]/
                const replacer = (match, p1) => {
                    return match.replace(p1, i);
                };
                const input_id = input.attr('id').replace(regex, replacer);
                const input_name = original_input_name.replace(regex, replacer);

                label.attr('for', input_id);
                input.attr('id', input_id);
                input.attr('name', input_name);

                if(input.attr('type') === 'checkbox') {
                    $(`input[type=hidden][name="${original_input_name}"]`, form).attr('name', input_name);
                }
            });
        };

        const renumberFieldsets = form => {
            $('fieldset', form).each((i, fieldset) => {
                renumberFormGroup(form, fieldset, i);
            });
        };

        const removeCommand = (form, e) => {
            const removeButton = $(e.target);
            const fieldset = removeButton.closest('fieldset');

            $(fieldset).remove();
            removeFieldSetHiddenField(form, fieldset);

            renumberFieldsets(form);
        };

        const removeFieldSetHiddenField = (form, fieldset) => {
            $('input[type="checkbox"]', fieldset).each((i, input) => {
                const input_name = $(input).attr('name');
                $(`input[type=hidden][name="${input_name}"]`, form).remove();
            });
        }

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

        $('.cmd.remove', todosForm).one('click', e => {
            removeCommand(todosForm, e);
        });

        $('#cmd-add', todosForm).click(_ => {
            const group = $('#todo-items-group');
            const tokenName = '__RequestVerificationToken';
            const token = $(`input[type="hidden"][name=${tokenName}]`, todosForm).val();
            const fieldsets = $('fieldset', group);
            const ids = [];
            fieldsets.each((i, fieldset) => {
                const id = parseInt($('input[name$=".Id"]', fieldset).val(), 10);
                ids.push(id);
            });
            const maxId = Math.max(...ids);

            $.ajax({
                url: '../../Todos/AddRow/',
                type: 'POST',
                data: { __RequestVerificationToken: token, maxId },
                error: (jqXHR, textStatus, errorThrown) => {
                    console.error({jqXHR, textStatus, errorThrown});
                },
                success: (data, textStatus, jqXHR) => {
                    console.info({data, textStatus, jqXHR});

                    group.append(data);

                    const fieldset = $('fieldset', group).last();
                    formatNewFieldGroup(fieldset);

                    $('.cmd.remove', fieldset).one('click', e => {
                        removeCommand(todosForm, e);
                    });

                    renumberFieldsets(todosForm);
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
