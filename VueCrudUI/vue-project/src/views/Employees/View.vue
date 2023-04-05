<template>
    <main>
        <div class="card">
            <div class="card-header">
                Employees
                <RouterLink to="/employees/create" class="btn btn-outline-success float-end ">Add Employee</RouterLink>
            </div>
            <div class="card-body">
                <div class="mb-3">
                    <div v-if="deletesuccess == true" class="alert alert-success" role="alert">
                        Employee Deleted Successfully
                    </div>
                </div>
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Gender</th>
                            <th>City</th>
                            <th>Is Active</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody v-if="employees.length > 0">
                        <tr v-for="(employee, index) in this.employees" :key="index">
                            <td>{{ employee.id }}</td>
                            <td>{{ employee.firstName }}</td>
                            <td>{{ employee.lastName }}</td>
                            <td>{{ employee.gender }}</td>
                            <td>{{ employee.city }}</td>
                            <td v-if="employee.isActive == true">Yes</td>
                            <td v-else>No</td>
                            <td>
                                <RouterLink :to="{ path: '/employees/edit/' + employee.id }" style="margin-left: 10px;"
                                    class="btn btn-outline-primary">
                                    Edit
                                </RouterLink>
                                <button type="button" @click="DeleteEmployee(employee.id)" style="margin-left: 20px;"
                                    class="btn btn-outline-danger">Delete</button>
                            </td>
                        </tr>
                    </tbody>
                    <tbody v-else>
                        Loading...
                    </tbody>
                </table>
            </div>
        </div>
    </main>
</template>

<script>
import axios from 'axios'
export default {
    name: 'employees',
    data() {
        return {
            deletesuccess: false,
            employees: []
        }
    },
    mounted() {
        this.getEmployees();
    },
    methods: {

        getEmployees() {
            axios.get('https://localhost:7130/api/Employees').then(res => {
                this.employees = res.data;
            });
        },
        DeleteEmployee(EmployeeID) {
            if (confirm("Are you sure you want to delete this Employee record?")) {
                axios.delete(`https://localhost:7130/api/Employees/${EmployeeID}`).then(res => {
                    alert("employee deleted successfully")
                    this.deletesuccess = true
                    setTimeout(x => this.deletesuccess = false, 3000)
                    this.getEmployees();
                })
            }
        }
    }
}
</script>