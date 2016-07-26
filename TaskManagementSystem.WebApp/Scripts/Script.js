$(".activationBtn").on("click", function () {  //Activates or disables users by adding or removing roles

    var email = $(this).attr("name");
    var thisElement = this;

    if ($(this).val() == "Activate") {

        var btnValue = "Disable";
        var url = '/Account/EnableUser'

        ToggleStatusOfUser(email, url, thisElement, btnValue);

    } else {
        var btnValue = "Activate";
        var url = '/Account/DisableUser'

        ToggleStatusOfUser(email, url, thisElement, btnValue);
    }
});


function ToggleStatusOfUser(email, url, thisElement, btnValue) {
    $.ajax({
        url: url,
        dataType: "json",
        type: 'POST',
        data: JSON.stringify({
            data: email
        }),
        contentType: 'application/json',
        success: function (data) {
            $(thisElement).prop('value', btnValue); 
        },
        error: function () {
            alert("error");
        }
    });
}


$(".ToDo").sortable({
    connectWith: ".InProgress, .InTestingQA, .Done",
    receive: function (ev, ui) {
        if (ui.sender.hasClass("Done")) {
            ui.sender.sortable("cancel");
        } else {
            var itemName = ui.item.children().html();
            var taskID = ui.item.attr("id");
            var taskProgress = $(this).attr("name");
            UpdateTaskProgress(taskID, taskProgress);
        }
    }
});

$(".InProgress").sortable({
    connectWith: ".ToDo, .InTestingQA, .Done",
    receive: function (ev, ui) {
        if (ui.sender.hasClass("InTestingQA") || ui.sender.hasClass("Done")) {
            ui.sender.sortable("cancel");
        } else {
            var itemName = ui.item.children().html();
            var taskID = ui.item.attr("id");
            var taskProgress = $(this).attr("name");
            UpdateTaskProgress(taskID, taskProgress);
        }
    }
});

$(".InTestingQA").sortable({
    connectWith: ".ToDo, .InProgress, .Done",
    receive: function (ev, ui) {
        if (ui.sender.hasClass("Done")) {
            ui.sender.sortable("cancel");
        } else {
            var itemName = ui.item.children().html();
            var taskID = ui.item.attr("id");
            var taskProgress = $(this).attr("name");
            UpdateTaskProgress(taskID, taskProgress);
        }
    }
});

$(".Done").sortable({
    connectWith: ".ToDo, .InTesting, .InProgress",
    receive: function (ev, ui) {
        if (ui.sender.hasClass("ToDo")) {
            ui.sender.sortable("cancel");
        } else {
            var itemName = ui.item.children().html();
            var taskID = ui.item.attr("id");
            var taskProgress = $(this).attr("name");
            UpdateTaskProgress(taskID, taskProgress);
        }

    }
});

function UpdateTaskProgress(taskID, taskProgress) {

    $.ajax({
        url: '/Tasks/ChangeProgress',
        dataType: "json",
        type: 'POST',
        traditional: true,
        data: {
            taskID: taskID,
            taskProgress: taskProgress,
        },
        //success: function (data) {
        //    alert("success");
        //},
        error: function () {
            alert("Task Progress has not been updated");
        }
    });
}