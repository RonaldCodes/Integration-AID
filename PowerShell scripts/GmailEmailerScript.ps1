#From Details, Connection Details
$username=”username@gmail.com”
$password="password"
$smtpServer = “smtp.gmail.com”
$msg = new-object Net.Mail.MailMessage

#Change port number for SSL to 587
$smtp = New-Object Net.Mail.SmtpClient($SmtpServer, 587) 

#Uncomment Next line for SSL  
$smtp.EnableSsl = $true

$smtp.Credentials = New-Object System.Net.NetworkCredential( $username, $password )

#From Address
$msg.From = $username
#To Address, Copy the below line for multiple recipients
$msg.To.Add(“receiver@anyEmailAddress.com”)

#Message Body
$msg.Body=”This is just a random message for the body of the Email”

#Message Subject
$msg.Subject = “Email with Multiple Attachments subject line”

#Multiple files in different locations
$locations = New-Object System.Collections.ArrayList
$fileslocation1=Get-ChildItem #PATH #“C:\Users\YaseenH\Desktop\Email\Attachment\*.csv”
$fileslocation2=Get-ChildItem #PATH #“C:\Users\YaseenH\Desktop\Email\Test\*.csv”
$locations.Add($fileslocation1)
$locations.Add($fileslocation2)

Foreach($location in $locations)
{
     Write-Host “Location File :- ” $location

        Foreach($file in $location)
        {
            Write-Host “Attaching File :- ” $file
            $attachment = New-Object System.Net.Mail.Attachment –ArgumentList $file
            $msg.Attachments.Add($attachment)
        }
}

$smtp.Send($msg)
$attachment.Dispose();
$msg.Dispose();