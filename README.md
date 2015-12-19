# first-repository
-- this is a console application that when is executed proceedes with this steps:
1. Reads the html text in a particular page, the url of which is in App.Config AppSettings
2. Parses the html in order to take only a specific table with a specific id or cssclass  ( in our case cssclass="maincontent")
3. Converts html table (parsed to it's simplest form) to an XElement Object
4. Inserts the data in the database

How it can be used:
It can be configured to run in a scheduler to fetch data at specific points in time
