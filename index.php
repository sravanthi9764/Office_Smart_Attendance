<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="css/style.css">
    <title>Employee Sign Up Form</title>
</head>
<body>
    <div class="signup-form">
        <h1> Signup Form </h1>
        <div class="txt">
            <form action="signup.php" method="POST">
                <i class="fa fa-user"></i>
                <input type="text" placeholder="UserName" name="username" required>
        </div>
		<div class="txt">
                <i class="fa fa-user"></i>
                <input type="text" placeholder="First Name" name="FName" required>
        </div>
		<div class="txt">
                <i class="fa fa-user"></i>
                <input type="text" placeholder="Last Name" name="LName" required>
        </div>
		<div class="txt">
                <i class="fa fa-user"></i>
                <input type="text" placeholder="Designation" name="Designation" required>
        </div>
        <div class="txt">
                <i class="fa fa-envelope"></i>
                <input type="email" placeholder="Email" name="Email" required>
        </div>
		<div class="txt">
                <i class="fa fa-phone"></i>
                <input type="text" placeholder="Phone" name="Phone" required>
        </div>
        <div class="txt">
                <i class="fa fa-lock"></i>
                <input type="password" placeholder="Password" name="Password" required>
        </div>
        <div class="txt">
                <i class="fa fa-lock"></i>
                <input type="password" placeholder="Confirm Password" name="CPassword" required>
        </div>
            <button class="btn" name="btn"> Signup </button>
        </form>
    </div>
</body>
</html>