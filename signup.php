<?php
require_once "config.php";
if (isset($_POST["btn"])) {
    $username = $_POST["username"];
    $FName = $_POST["FName"];
    $LName = $_POST["LName"];
    $Designation = $_POST["Designation"];
    $Phone = $_POST["Phone"];
    $Email = $_POST["Email"];
    $Password = $_POST["Password"];
    $CPassword = $_POST["CPassword"];

    if (
        empty($username) ||
        empty($FName) ||
        empty($LName) ||
        empty($Designation) ||
        empty($Phone) ||
        empty($Email) ||
        empty($LName) ||
        empty($Password) ||
        empty($CPassword)
    ) {
        echo " All Information Required ";
    } else {
        if ($Password != $CPassword) {
            echo " Password Mismatch ";
        } else {
            $pass = md5($Password);
            $query = "select count(*) as counts from employees where username = '$username'";
            $result = mysqli_query($con, $query);
            $counts = 0;

            while ($row = mysqli_fetch_assoc($result)) {
                $counts = $row["counts"];
            }

            if ($counts == 0) {
                $query = "insert into employees (username,FName,LName,Designation,Phone,email,password) values ('$username','$FName','$LName','$Designation','$Phone','$Email','$Password')";
                $result = mysqli_query($con, $query);

                if ($result) {
                    echo " Employee SignUp Successful ";
                } else {
                    echo " Something Went Wrong ";
                }
            } else {
                echo " Duplicate UserName Found ";
            }
        }
    }
}
?>
