<template>
    <main>
        <div ref="myModal" class="modal">
            <div class="modal-content">
                <span @click="this.$refs.myModal.style = 'display:none'" class="close">&times;</span>
                <div style="margin: auto;">
                    <p>Are you Sure you want to delete <br> {{ this.delinfo.deletegender }}{{ this.delinfo.deleteName }}</p>
                    <button @click="DeleteEmployee(this.delinfo.deleteid)" class="btn btn-outline-primary"
                        style="width : 50px; margin-left : 55px; margin-right: 4px;" value="Yes">Yes</button>
                    <button @click="this.$refs.myModal.style = 'display:none'" class="btn btn-outline-primary"
                        style="width : 50px; margin-left: 4px;" value="No">No</button>
                </div>
            </div>
        </div>
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
                <div class="mb-3">
                    <div v-if="validationsuccess == true" class="alert alert-success" role="alert">
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
                                <button type="button" id="myBtn"
                                    @click="openModal(employee.id, employee.firstName, employee.gender)"
                                    style="margin-left: 20px;" class="btn btn-outline-danger"> Delete</button>
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
import { ref } from 'vue'
export default {
    name: 'employees',
    data() {
        return {
            modalvisible: false,
            deletesuccess: false,
            validationsuccess: false,
            delinfo: {
                deleteid: '',
                deleteName: '',
                deletegender: ''
            },
            employees: [],
            delemp: {
                id: '',
                firstName: '',
                lastName: '',
                gender: '',
                city: '',
                isActive: ''
            }
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
        getEmployeeId(EmployeeId) {
            axios.get(`https://localhost:7130/api/Employees/${EmployeeId}`).then(res => {
                this.delemp = res.data;
            });
        },
        openModal(employeeId, employeeFirstName, employeeGender) {
            let button = this.$refs.myModal
            button.style = 'display:block'
            this.delinfo.deleteid = employeeId
            this.delinfo.deleteName = employeeFirstName
            this.delinfo.deletegender = employeeGender == "Male" ? "Mr." : "Mrs."
        },
        DeleteEmployee(EmployeeID) {
            this.getEmployeeId(EmployeeID)
            axios.delete(`https://localhost:7130/api/Employees/${EmployeeID}`).then(res => {
                console.log(this.deleteques)
                this.modalvisible = false
                this.deletesuccess = true
                this.$refs.myModal.style = 'display:none'
                setTimeout(x => this.deletesuccess = false, 1500)
                this.getEmployees();
            });
        }
    }
}
</script>


<style scoped>
.modal {
    display: none;
    z-index: 1;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    margin: auto;

}

.modal-content {
    background-color: #fefefe;
    margin: 15% auto;
    padding: 20px;
    border: 1px solid #888;
    width: 20%;

}

.close {
    color: #aaa;
    float: right;
    font-size: 28px;
    font-weight: bold;
}

.close:hover,
.close:focus {
    color: black;
    text-decoration: none;
    cursor: pointer;

}
</style>