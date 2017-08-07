#Make Sure Outlook is closed or opened as administrator
$Outlook = New-Object -ComObject Outlook.Application
$Mail = $Outlook.CreateItem(0)
#To Address, Copy the below line for multiple recipients
$Mail.To = "receiver@anyEmailAddress.com"
$Mail.Subject = "Email with Multiple Attachments subject line"
$Mail.Body ="This is just a random message for the body of the Email"

#Attachments:: Should have specific paths and name of files
#$Mail.Attachments.Add(PATH)   #$Mail.Attachments.Add("C:\Users\YaseenH\Desktop\Email\Test\invoice.csv")
#$Mail.Attachments.Add(PATH)    #$Mail.Attachments.Add("C:\Users\YaseenH\Desktop\Email\Attachment\purchasesorder.csv")

$Mail.Send()

#Take a look at the link below o automate sending emails directly from outlook
#https://www.extendoffice.com/documents/outlook/1567-outlook-send-schedule-recurring-email.html