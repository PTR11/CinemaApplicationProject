<template>
    <div class="card col-sm-5 m-2 mx-auto m-1 border-3 border-light bg-warning text-light">
        <form>
            <h3>Sign In</h3>

            <div class="form-group">
                <label>Email address</label>
                <input type="email" class="form-control form-control-lg" />
            </div>

            <div class="form-group">
                <label>Password</label>
                <input type="password" class="form-control form-control-lg" />
            </div>

            <button style="border: 1px solid white; border-radius: 25px;" class="btn btn-dark btn-lg btn-block">Sign In</button>
            
            <p class="forgot-password text-right mt-2 mb-4">
                <router-link to="/forgot-password">Forgot password ?</router-link>
            </p>
        </form>
    </div>
</template>

<script>
    export default {
        data() {
            return {}
        }
    }
</script>