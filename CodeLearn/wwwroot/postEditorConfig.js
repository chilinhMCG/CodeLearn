window.postEditorConfig = {
    content_css: [
        'https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap',
        '/css/bootstrap/bootstrap.min.css',
        '/_content/MudBlazor/MudBlazor.min.css',
        '/css/tinymce/classic_mode_content.css'
    ],
    menubar: false,
    statusbar: false,
    toolbar_sticky: true,
    toolbar_location: 'bottom',
    min_height: 300,
    placeholder: "Nội dung bài viết...",
    plugins: ['autoresize', 'lists', 'image', 'imagetools', 'media', 'link', 'codesample', 'autolink', 'paste', 'table'],
    image_caption: true,
    paste_data_images: true,
    toolbar: [
        'formatselect forecolor backcolor | alignleft aligncenter alignright alignfull | outdent indent | undo redo',
        'bold italic underline | blockquote codesample | numlist bullist table | image media | link unlink'
    ],
};
