"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gradeAssignmentsHub").build();
connection.serverTimeoutInMilliseconds = 100000;
connection.on("ShowLiveGradeAssignments", function (liveGradeAssignments) {
    var table = document.getElementById("liveGradeAssignmentsTable");
    for (var i = 0, row; row = table.rows[i]; i++) {
        row.classList.remove('table-info');
    }

    for (var i = 0; i < liveGradeAssignments.length; i++) {
        var liveGradeAssignment = liveGradeAssignments[i];
        //add it
        var row = table.insertRow(table.rows.length);
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        var cell4 = row.insertCell(3);
        cell1.innerHTML = liveGradeAssignment.studentId;
        cell2.innerHTML = liveGradeAssignment.professorId;
        cell3.innerHTML = liveGradeAssignment.lessonId;
        cell4.innerHTML = liveGradeAssignment.grade;
        row.classList.add('table-info');
    }
    window.scrollTo(0, table.offsetHeight);
});

connection.on("ResetLiveGradeAssignments", function () {
    var table = document.getElementById("liveGradeAssignmentsTable");
    while (table.rows.length > 1) {
        table.deleteRow(1);
    }
});

connection.start().then(function () {
    var table = document.getElementById("liveGradeAssignmentsTable");
    while (table.rows.length > 1) {
        table.deleteRow(1);
    }
    connection.invoke("SendLiveGradeAssignmentsToClient").catch(err => console.error(err.toString()));
}).catch(function (err) {
    return console.error(err.toString());
});

connection.onclose(function (e) {
    connection.start().then(function () {
    }).catch(function (err) {
        return console.error(err.toString());
    });

});

