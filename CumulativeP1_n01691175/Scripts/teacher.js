
function AddTeacher() {

	//goal: send a request which looks like this:
	//POST : https://localhost:44386/api/TeacherData/AddTeacher/
	//with POST data of Teacher first name, last name, employee number and salary.

	var URL = "https://localhost:44386/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var Salary = document.getElementById('Salary').value;



	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"EmployeeNumber": EmployeeNumber,
		"Salary": Salary
	};
	console.log("Logged data", TeacherData);

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {

		if (rq.readyState == 4 && rq.status == 200) {
			console.log("Sent unsuccessful");
		} else
			console.log("Failed: " + rq.status + " - " + rq.statusText);
		}


	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));
}
