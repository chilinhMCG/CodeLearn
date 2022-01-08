//Hàm này lấy tạo config obj cho một editor, 
//Khi config obj đã tạo xong sẽ gọi lại component.
//Và khi editor đã được load xong sử dụng config obj được tạo, sẽ gọi lại component lần nữa.
window.createEditorConfigObj = (objRef, id, baseConfigName) => {
    let configObjName = `editorConfig-${id}`
    window[configObjName] = {
        ...window[baseConfigName],

        setup: function (editor) {
            editor.on('LoadContent', function (e) {
                objRef.invokeMethodAsync('OnEditorLoaded');
            });
        }
    }

    objRef.invokeMethodAsync('OnConfigObjReady', configObjName);
};


window.disposeEditorConfigObj = (id) => {
    delete window[`editorConfig-${id}`];
}