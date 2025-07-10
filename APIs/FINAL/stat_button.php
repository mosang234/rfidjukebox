<?php

include_once('connects.php');

$query = "SELECT * FROM `button`";
$check=mysqli_query($con,$query);
$row=mysqli_num_rows($check);


if($check == FALSE) { 
    echo ".".$row."."; // TODO: better error handling
}

while($row=mysqli_fetch_array($check))
  	{	
	echo $row['status_song'];
  	}
  

?>