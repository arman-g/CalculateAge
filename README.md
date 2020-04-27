# A simple extesion method to calculate a person's age
### Features
* Calculates a person's age and returns years, months and days information.
* Supports leap years and leaper date of birth (Feb 29).

### Output

> **L** - Leap Year  
> **N** - Normal Year
> ```
> -------- General Cases ---------
> 01/01/2000:L - 01/01/2000:L Age: 0yr., 0mos., 0d
> 01/01/2000:L - 01/02/2000:L Age: 0yr., 0mos., 1d
> 01/01/2000:L - 02/01/2000:L Age: 0yr., 1mos., 0d
> 01/01/2000:L - 12/31/2000:L Age: 0yr., 11mos., 30d
> 01/01/2000:L - 01/01/2001:N Age: 1yr., 0mos., 0d
> 04/22/2001:N - 10/27/2019:N Age: 18yr., 6mos., 5d
> 04/22/2001:N - 01/01/2020:L Age: 18yr., 8mos., 10d
> 04/22/2001:N - 02/17/2020:L Age: 18yr., 9mos., 26d
> 04/22/2001:N - 04/22/2020:L Age: 19yr., 0mos., 0d
> 04/22/2001:N - 07/07/2020:L Age: 19yr., 2mos., 15d
> 
> -------- Special  Cases (Feb 29) ---------
> 02/29/1960:L - 02/28/2020:L Age: 59yr., 11mos., 30d
> 02/29/1960:L - 02/29/2020:L Age: 60yr., 0mos., 0d
> 02/29/1960:L - 03/01/2020:L Age: 60yr., 0mos., 1d
> 02/29/1960:L - 02/27/2021:N Age: 60yr., 11mos., 29d
> 02/29/1960:L - 02/28/2021:N Age: 61yr., 0mos., 0d
> 02/29/1960:L - 03/01/2021:N Age: 61yr., 0mos., 1d
> ```
