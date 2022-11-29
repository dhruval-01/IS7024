<img src="https://github.com/dhruval-01/IS7024/blob/master/logo-no-background.png" width="350" alt="accessibility text">

# Introduction
Next works here...

Are you a job seeker? Do you know which are the most booming jobs in the Cincinnati public sector? Do you know what job profiles pay the most? Our website will help you clear all your questions and help you get the right job...

* Display Job Opportunities available in the city's public sector  
* Give insights into the salaries paid for different job roles
* Helps you find a job in your preferred job location 
* Shows you the recent hiring trends for the role you are seeking
* Job Type (Full Time or Part Time)
* Helps you find your interest Department
* With live recruitment analytics gets insigts of the job market 

# DataFeeds

<a href="https://data.cincinnati-oh.gov/Efficient-Service-Delivery/City-of-Cincinnati-Employees-w-Salaries/wmj4-ygbf">City of Cincinnati Employees w/ Salaries</a><br/>
<a href="https://data.cincinnati-oh.gov/Efficient-Service-Delivery/City-of-Cincinnati-Department-Information/txnn-6e6x">City of CincinnatI Department Information</a>

# Requirements
As a job seeker, I want to know more about the job opportunities in the public domain, I want to check the various roles which are being offered in the city of Cincinnati public domain based on the range of salaries being offered, job type, department and my preferred job location. This will help me decide my career path and also the role which are trending and in demand.

## Dependencies

The city of Cincinnati provides us the dataset which has the details of all the employees, their full names, department, position title, full-time employee status, employee age range, employee race and annual salary rate.

## Examples

Assuming that an individual is eligible to take up a job role of a <strong>Police Sergeant.</strong> He/she can then go on our website and get insights on the statistics that will help the user better plan his/her career as a Police Sergeant. The user can then filter out precincts depending on his/her preferences and role availability,<strong> thus saving him/her some precious time.</strong> Additionally, the user can only apply to the precinct that provides minimum expected salaries, has a desirable number of hours shift, and is in a preferred location, <strong>thus increasing his/her chances of placement</strong>

1.1
<strong>Given</strong> a feed of employee salary data and salary schedule is available <br>
   <strong>When</strong> I look for "Senior Engineer-EXM" <br>
   <strong>Then</strong> I should receive at least one result with these attributes:<br>
   Department name: "CWW Plant Plan & Spec. Studies"<br>
   Location : "WAEPLANSTD"	<br>
   Annual Package : 101,391.231 <br>
 
1.2
<strong>Given</strong> a feed of employee salary data and salary schedule is available<br/>
<strong>When</strong> : 
		Select the JobTitle "Senior Admin Spec-EXM" <br/>
		<strong>Then:</strong>
		Should receive the salary Plan Desc, Annual Salary Rate, Job Family 
 
 1.3
<strong>Given</strong> a feed of employee salary data and salary schedule is available <br/>
<strong>When</strong> I look for “dbshdghsdu” <br/>
<strong>Then:</strong> I should receive no results (an empty list)
   
   

# Scrum Roles

* Scrum Master : Yash Karve
* Front End Developer : Lokesh RS
* Back-end Developer : Dhruval Chheda
* Back-end Developer : Adarsh Joshi

# Weekly Meeting
 
 Wednesday at 4pm on Teams
