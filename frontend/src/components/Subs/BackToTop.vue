<template>
<div>
  <Anchor class="backTop" container="#root-div">
        <AnchorLink href="#topAnchor" title="ㅿ"></AnchorLink>
    </Anchor>
</div>
</template>
<script>
export default {
  name: "backToTop"
};
</script>

<style>
.backTop {
  position: fixed;
  right: 20px;
  bottom: 80px;
  font-size: 50px;
  opacity: 0.6;
  /* font-weight: bold; */
}
.backTop:hover{
  opacity: 1;
}
</style>
